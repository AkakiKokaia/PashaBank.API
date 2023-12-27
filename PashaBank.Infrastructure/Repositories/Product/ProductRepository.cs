using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces;
using PashaBank.Domain.Interfaces.Repositories.Product;

namespace PashaBank.Infrastructure.Repositories.Product
{
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
    {
        private readonly IUnitOfWork _uow;

        public ProductRepository(
            IUnitOfWork uow,
            PashaBankDbContext context) : base(context)
        {
            _uow = uow;
        }
    }
}
