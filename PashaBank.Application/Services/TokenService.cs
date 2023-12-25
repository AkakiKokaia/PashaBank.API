using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces;
using PashaBank.Domain.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PashaBank.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey tokenKey;
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TokenService(IConfiguration configuration, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<(string AccessToken, string RefreshToken)> CreateReturnCredentials(UserEntity user, CancellationToken cancellationToken)
        {
            var claims = CreateUserClaims(user);

            var creds = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature);

            var AccessTokenExpirationMinutes = configuration["JwtSettings:AccessTokenExpirationMinutes"];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = configuration["JwtSettings:Issuer"],
                Issuer = configuration["JwtSettings:Issuer"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(AccessTokenExpirationMinutes)),
                SigningCredentials = creds,
            };

            var TokenHandler = new JwtSecurityTokenHandler();

            var refreshTokens = unitOfWork.refreshTokenRepository.GetAllWhere(x => x.UserId == user.Id);

            await DisableOldRefreshTokens(user.RefreshTokens.ToList());

            var token = TokenHandler.CreateToken(tokenDescriptor);
            // Writes JWT Token
            var tokenResult = TokenHandler.WriteToken(token);
            // Creates Refresh Token
            var refreshToken = await CreateRefreshToken(user.Id);

            await SaveRefreshTokenInDb(refreshToken, user.Id, cancellationToken);

            return (tokenResult, refreshToken.RefreshToken);
        }

        public async Task<(string AccessToken, string RefreshToken)> RefreshAccessToken(string expiredAccessToken, string refreshToken, CancellationToken cancellationToken)
        {
            var claimsPrincipal = GetPrincipalFromExpiredToken(expiredAccessToken);
            var userId = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.NameId).Value;
            // validate the refresh token
            var isValidRefreshToken = await ValidateRefreshToken(Guid.Parse(userId), refreshToken);
            // handle invalid refresh token
            if (!isValidRefreshToken.isValid) throw new SecurityTokenException("Invalid refresh token.");
            // generate a new access token
            var newAccessToken = await CreateReturnCredentials(isValidRefreshToken.user, cancellationToken);

            return newAccessToken;
        }

        private List<Claim> CreateUserClaims(UserEntity user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Roles.FirstOrDefault().Role.Name),
            };
        }

        private async Task DisableOldRefreshTokens(List<RefreshTokenEntity> refreshTokens)
        {
            if (refreshTokens.Where(x => x.Revoked != true).Any()) 
            {
                refreshTokens
                    .Where(x => x.Revoked != true)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.Revoked = true;
                        x.RevokedAt = DateTime.Now;
                    });
                await unitOfWork.refreshTokenRepository.UpdateRange(refreshTokens);
                await unitOfWork.refreshTokenRepository.SaveChangesAsync();
            }
        }

        private async Task<RefreshTokenEntity> CreateRefreshToken(Guid userId)
        {
            var refreshToken = new RefreshTokenEntity
            {
                UserId = userId,
                RefreshToken = Guid.NewGuid().ToString("N"),
                ExpirationTime = DateTime.Now.AddDays(int.Parse(configuration["JwtSettings:RefreshTokenExpirationDays"])),
            };

            // Return the refresh token
            return refreshToken;
        }

        private async Task SaveRefreshTokenInDb(RefreshTokenEntity newEntity, Guid userId, CancellationToken cancellationToken)
        {
            var existingTokens = await unitOfWork.refreshTokenRepository.GetAllWhereAsync(x => x.UserId == userId);
            await DisableOldRefreshTokens(existingTokens.ToList());
            await unitOfWork.refreshTokenRepository.Add(newEntity, cancellationToken);
        }

        private async Task<(bool isValid, UserEntity? user)> ValidateRefreshToken(Guid userId, string refreshToken)
        {
            var user = await unitOfWork.userRepository.FindFirst(x => x.Id == userId);

            // Check if user and refreshtoken exists and sent token is in correct format
            if (user == null
                || !user.RefreshTokens.Any()
                || !user.RefreshTokens.OrderByDescending(x => x.CreatedAt).Any(x => x.RefreshToken == refreshToken))
            {
                throw new KeyNotFoundException("Incorrect refresh token data");
            }

            if (user.RefreshTokens.Any(x => x.RefreshToken == refreshToken)
                && user.RefreshTokens.FirstOrDefault(x => x.RefreshToken == refreshToken).ExpirationTime < DateTime.Now)
            {
                throw new SecurityTokenException("Token validity expired!");
            }

            var userRefreshTokens = await unitOfWork.userRepository
                                                    .GetAllWhere(x => x.Id == user.Id)
                                                    .Include(x => x.RefreshTokens.Where(x => x.Revoked != true))
                                                    .FirstOrDefaultAsync();

            await DisableOldRefreshTokens(userRefreshTokens.RefreshTokens.ToList());

            return (true, user);
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Issuer"],
                    IssuerSigningKey = tokenKey
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claims = jwtToken.Claims;

                return new ClaimsPrincipal(new ClaimsIdentity(claims));
            }
            catch (Exception)
            {
                throw new SecurityTokenException("Invalid Token");
            }
        }
    }
}
