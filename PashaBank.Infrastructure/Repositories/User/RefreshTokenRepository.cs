using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces.Repositories.RefreshToken;
using PashaBank.Infrastructure;

namespace PashaBank.Infrastructure.Repositories.User
{
    public class RefreshTokenRepository : GenericRepository<RefreshTokenEntity>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(
            PashaBankDbContext context) : base(context) { }
    }
}
