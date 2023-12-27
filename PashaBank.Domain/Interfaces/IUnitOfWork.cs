using PashaBank.Domain.Interfaces.Repositories.User;

namespace PashaBank.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository userRepository { get; }
        IUserRoleRepository userRoleRepository { get; }
    }
}
