using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PashaBank.Domain.Entities;

namespace PashaBank.Infrastructure
{
    public class DbInitializer
    {
        public static void InitializeDatabase(IServiceProvider serviceProvider, PashaBankDbContext context)
        {

            #region Migrations
            using IServiceScope serviceScope = serviceProvider.CreateScope();

            try
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
                var initializer = new DbInitializer();
                initializer.Seed(context);
            }
            catch (Exception ex)
            {
                var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<PashaBankDbContext>>();



                logger.LogError(ex, "An error occurred while migrating or seeding the database.");



                throw;
            }
            #endregion
        }

        #region Seeding

        private void Seed(PashaBankDbContext context)
        {
            context.Database.EnsureCreated();
            SeedRoles(context);
        }
        //NEED SEED
        private void SeedRoles(PashaBankDbContext context)
        {
            if (!context.Roles.Any())
            {
                RoleEntity role = new();

                role = new RoleEntity
                {
                    Id = Guid.Parse("4edf7903-ee5f-4b74-ba56-d8e579771f2a"),
                    Name = "Distributor",
                    NormalizedName = "DISTRIBUTOR",
                    ConcurrencyStamp = null
                };
                context.Add(role);
                role = new RoleEntity
                {
                    Id = Guid.Parse("0a194ecb-fba2-4a88-915f-1bc41a4c3555"),
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = null
                };
                context.Add(role);
                role = new RoleEntity
                {
                    Id = Guid.Parse("0bfecbe5-e358-46af-90c5-076c2f12c220"),
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = null
                };
                context.Add(role);
                context.SaveChanges();
            }
        }

        #endregion
    }
}
