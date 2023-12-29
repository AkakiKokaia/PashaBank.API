using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PashaBank.Application.Behaviors;
using PashaBank.Application.Services;
using PashaBank.Domain.Interfaces;
using PashaBank.Domain.Interfaces.Repositories.Product;
using PashaBank.Domain.Interfaces.Repositories.ProductSales;
using PashaBank.Domain.Interfaces.Services;
using PashaBank.Infrastructure;
using PashaBank.Infrastructure.Repositories;
using PashaBank.Infrastructure.Repositories.Product;
using PashaBank.Infrastructure.Repositories.ProductSale;
using PersonIdentificationDirectory.Utility.Behaviors;
using System.Reflection;

namespace PashaBank.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            #region Repositories

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductSaleRepository, ProductSaleRepository>();

            #endregion

            #region Services

            services.AddTransient<IUserService, UserService>();
            services.AddScoped<ITransactionBehaviour>(p => p.GetRequiredService<PashaBankDbContext>());

            #endregion
        }
    }
}
