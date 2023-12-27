using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces;
using PashaBank.Infrastructure;

namespace PashaBank.Application.Services
{
    public class UserService
    {
        private readonly PashaBankDbContext _context;

        public UserService(PashaBankDbContext context)
        {
            _context = context;
        }

        public UserEntity GetSixthStageParent(Guid? recommendedById)
        {
            var entity = _context.Users.Find(recommendedById);
            return GetParentRecursive(entity, 6);
        }

        private UserEntity GetParentRecursive(UserEntity entity, int level)
        {
            if(level <= 0 || entity == null)
            {
                return entity;
            }

            //_context.Entry(entity).Reference(e => e.RecommendedById).Load();
            //return GetParentRecursive(entity.RecommendedById, level - 1);
            return null;
        }
    }
}
