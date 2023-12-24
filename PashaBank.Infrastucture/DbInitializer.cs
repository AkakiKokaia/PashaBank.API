using Microsoft.EntityFrameworkCore;
using PashaBank.Domain.Entities;

namespace PashaBank.Infrastucture
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
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = null
                };
                context.Add(role);
                role = new RoleEntity
                {
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
