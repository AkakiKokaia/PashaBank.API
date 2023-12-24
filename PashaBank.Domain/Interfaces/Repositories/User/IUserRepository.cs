using PashaBank.Domain.Entities;

namespace PashaBank.Domain.Interfaces.Repositories.User
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task CreateUser(UserEntity user, string password);
        Task<UserEntity> ValidateUser(string email, string password);
        Task<bool> ValidateUserAvailability(UserEntity user, CancellationToken cancellationToken = default);
    }
}
