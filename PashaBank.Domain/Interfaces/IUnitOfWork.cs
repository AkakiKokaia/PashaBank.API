using PashaBank.Domain.Interfaces.Repositories.Product;
using PashaBank.Domain.Interfaces.Repositories.ProductSales;
using PashaBank.Domain.Interfaces.Repositories.User;

namespace PashaBank.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository userRepository { get; }
        IUserRoleRepository userRoleRepository { get; }
        IProductRepository productRepository { get; }
        IProductSaleRepository productSalesRepository { get; }
    }
}
