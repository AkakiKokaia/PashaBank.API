using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces;
using PashaBank.Domain.Interfaces.Repositories.ProductSales;

namespace PashaBank.Infrastructure.Repositories.ProductSale
{
    public class ProductSaleRepository : GenericRepository<ProductSalesEntity>, IProductSaleRepository
    {
        private readonly IUnitOfWork _uow;

        public ProductSaleRepository(
            IUnitOfWork uow,
            PashaBankDbContext context) : base(context)
        {
            _uow = uow;
        }
    }
}
