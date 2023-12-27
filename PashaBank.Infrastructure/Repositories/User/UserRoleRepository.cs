using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces.Repositories.User;

namespace PashaBank.Infrastructure.Repositories.User
{
    internal class UserRoleRepository : GenericRepository<UserRoleEntity>, IUserRoleRepository
    {
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly PashaBankDbContext _dbContext;

        public UserRoleRepository(RoleManager<RoleEntity> roleManager, IConfiguration configuration, PashaBankDbContext context) : base(context)
        {
            _roleManager = roleManager;
            _configuration = configuration;
            _dbContext = context;
        }

        public async Task AddToRoleAsync(UserEntity user, Guid roleId)
        {
            var test = await _dbContext.UserRoles.AddAsync(new UserRoleEntity() { UserId = user.Id, RoleId = roleId });
            await _dbContext.SaveChangesAsync();
        }
    }
}
