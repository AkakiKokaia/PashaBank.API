using PashaBank.Domain.Interfaces.Repositories.RefreshToken;
using PashaBank.Domain.Interfaces.Repositories.User;

namespace PashaBank.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRefreshTokenRepository refreshTokenRepository { get; }
        IUserRepository userRepository { get; }
        IUserRoleRepository userRoleRepository { get; }
    }
}
