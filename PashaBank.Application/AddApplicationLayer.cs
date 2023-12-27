using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PashaBank.Application.Behaviors;
using PashaBank.Domain.Interfaces;
using PashaBank.Infrastructure.Repositories;
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

            #region Services
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion
        }
    }
}
