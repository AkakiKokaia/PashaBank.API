﻿using Microsoft.Extensions.DependencyInjection;
using PashaBank.Domain.Interfaces;
using PashaBank.Domain.Interfaces.Repositories.Product;
using PashaBank.Domain.Interfaces.Repositories.ProductSales;
using PashaBank.Domain.Interfaces.Repositories.User;

namespace PashaBank.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IServiceProvider _serviceProvider;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
           _serviceProvider = serviceProvider;
        }

        #region Repositories

        public IUserRepository userRepository => _serviceProvider.GetService<IUserRepository>();
        public IUserRoleRepository userRoleRepository => _serviceProvider.GetService<IUserRoleRepository>();
        public IProductRepository productRepository => _serviceProvider.GetService<IProductRepository>();
        public IProductSaleRepository productSalesRepository => _serviceProvider.GetService<IProductSaleRepository>();

        #endregion
    }
}
