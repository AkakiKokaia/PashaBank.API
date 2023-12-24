using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces;
using PashaBank.Domain.Interfaces.Repositories.RefreshToken;
using PashaBank.Domain.Interfaces.Repositories.User;
using PashaBank.Domain.Interfaces.Services;
using PashaBank.Infrastructure.Repositories;
using PashaBank.Infrastructure.Repositories.User;
using PashaBank.Infrastucture;
using System.Text;

namespace PashaBank.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITokenService, ITokenService>();
            #endregion

            #region Repositories

            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserRoleRepository, UserRoleRepository>();

            #endregion

            services.AddOptions();

            services.AddDbContext<PashaBankDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x =>
                {
                    x.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
                }).LogTo(Console.WriteLine, LogLevel.Information);
            }, ServiceLifetime.Transient);
        }

        public static void AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<UserEntity, RoleEntity>(options =>
            {
                options.Lockout.AllowedForNewUsers = false;
            })
            .AddEntityFrameworkStores<PashaBankDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Configure password requirements
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            });

            //services.AddScoped<ITokenService, TokenService>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
                };
            });
            services.AddAuthorization();
        }
    }
}
