using PashaBank.Domain.Entities;

namespace PashaBank.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        Task<(string AccessToken, string RefreshToken)> CreateReturnCredentials(UserEntity user, CancellationToken cancellationToken);
        Task<(string AccessToken, string RefreshToken)> RefreshAccessToken(string expiredAccessToken, string refreshToken, CancellationToken cancellationToken);
    }
}
