using PashaBank.Domain.Interfaces.Services;
using PashaBank.Infrastructure;

namespace PashaBank.Application.Services
{
    public class UserService : IUserService
    {
        private readonly PashaBankDbContext _context;

        public UserService(PashaBankDbContext context)
        {
            _context = context;
        }

        public bool IsRecommendationAllowed(Guid? recommendedById, int level)
        {
            var entity = _context.Users.Find(recommendedById);
            if (level == 0 && entity != null)
            {
                return false;
            }
            if (entity.RecommendedById == null)
            {
                return true;
            }
            return IsRecommendationAllowed(entity.RecommendedById, level - 1);
        }

        public bool HasRecommendedMoreThanThree(Guid? recommendedById)
        {
            var entity = _context.Users.Where(x => x.RecommendedById == recommendedById).Count();
            if (entity >= 3)
            {
                return false;
            }
            return true;
        }
    }
}
