using PashaBank.Domain.Entities;

namespace PashaBank.Domain.Interfaces.Repositories.User
{
    public interface IUserRoleRepository : IGenericRepository<UserRoleEntity>
    {
        Task AddToRoleAsync(UserEntity user, Guid roleId);
    }
}
