using PashaBank.Domain.Entities;

namespace PashaBank.Domain.Interfaces.Services
{
    public interface IUserService
    {
        bool IsRecommendationAllowed(Guid? recommendedById, int level);
        bool HasRecommendedMoreThanThree(Guid? recommendedById);
        Task AccummulatedBonusCalculator(List<ProductSalesEntity> productSales);
        Task<List<UserEntity>> GetUsersByProductSales(List<ProductSalesEntity> productSales);
    }
}
