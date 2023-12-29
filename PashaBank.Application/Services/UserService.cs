using Microsoft.EntityFrameworkCore;
using PashaBank.Domain.Entities;
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
            var entity = _context.Users.Count(x => x.RecommendedById == recommendedById);

            if (entity >= 3)
            {
                return false;
            }
            return true;
        }

        public async Task AccummulatedBonusCalculator(List<ProductSalesEntity> productSales)
        {
            var users = await GetUsersByProductSales(productSales);

            foreach (var user in users)
            {
                var descendants = new Dictionary<UserEntity, int>();
                AddAllDescendants(user.Id, users, ref descendants);

                var bonus = CalculateBonus(user.Id, descendants, productSales);
                user.AddBonus(bonus);
                _context.Update(user);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserEntity>> GetUsersByProductSales(List<ProductSalesEntity> productSales)
        {
            var userIdList = productSales.Select(x => x.UserId).Distinct().ToList();
            return await _context.Users.Where(x => userIdList.Contains(x.Id)).ToListAsync();
        }

        private void AddAllDescendants(Guid userId, List<UserEntity> users, ref Dictionary<UserEntity, int> descendants, int level = 0)
        {
            if (level == 3) return;
            level++;
            var children = users.Where(u => u.RecommendedById == userId).ToList();
            foreach (var child in children)
            {
                descendants.Add(child, level);
                AddAllDescendants(child.Id, users, ref descendants, level);
            }
        }

        public decimal CalculateBonus(Guid userId, Dictionary<UserEntity, int> descendants, List<ProductSalesEntity> productSales)
        {
            decimal finalPrice = GetPriceByDescendantLevel(userId, productSales);

            foreach (var child in descendants)
            {
                finalPrice += GetPriceByDescendantLevel(child.Key.Id, productSales, child.Value);
            }

            return finalPrice;
        }

        public decimal GetPriceByDescendantLevel(Guid userId, List<ProductSalesEntity> productSales, int level = 0)
        {
            var percentage = 10;
            switch (level)
            {
                case 1:
                    percentage = 5;
                    break;
                case 2:
                    percentage = 1;
                    break;
            }

            foreach (var sale in productSales)
            {
                sale.SetWasCalculated();
                _context.Update(sale);
            }

            var userSalesTotal = productSales.Where(x => x.UserId == userId).Sum(x => x.TotalPrice);

            return userSalesTotal * percentage / 100;
        }
    }
}
