using PashaBank.Domain.Interfaces;
using PashaBank.Domain.Interfaces.Repositories.RefreshToken;
using PashaBank.Domain.Interfaces.Repositories.User;

namespace PashaBank.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IServiceProvider _serviceProvider;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
           _serviceProvider = serviceProvider;
        }

        #region Repositories

        public IRefreshTokenRepository refreshTokenRepository => _serviceProvider.GetService<IRefreshTokenRepository>();
        public IUserRepository userRepository => _serviceProvider.GetService<IUserRepository>();
        public IUserRoleRepository userRoleRepository => _serviceProvider.GetService<IUserRoleRepository>();

        #endregion
    }
}
