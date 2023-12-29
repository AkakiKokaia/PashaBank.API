using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PashaBank.Domain.Entities;
using PashaBank.Infrastructure.Constants;

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
            SeedProducts(context);
            SeedUsers(context);
            SeedUserRoles(context);
            SeedProductSales(context);
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
                    Id = Guid.Parse("0bfecbe5-e358-46af-90c5-076c2f12c220"),
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = null
                };
                context.Add(role);
                context.SaveChanges();
            }
        }

        private void SeedProducts(PashaBankDbContext context)
        {
            if (!context.Products.Any())
            {
                ProductEntity products = new();

                products = new ProductEntity
                {
                    Id = Guid.Parse("4edf7903-ee5f-4b74-ba56-d8e579771f2a"),
                    ProductCode = "Z123",
                    ProductName = "TestProduct",
                    PricePerUnit = 25,
                };
                context.Add(products);
                context.SaveChanges();
            }
        }

        private void SeedUsers(PashaBankDbContext context)
        {
            if (!context.Users.Any())
            {
                UserEntity users = new();

                users = new UserEntity
                {
                    Id = Guid.Parse("DD089B36-4F62-4ACF-D021-08DC07AE017F"),
                    FirstName = "John",
                    Surname = "Slasher",
                    DateOfBirth = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    Gender = 0,
                    PhotoURL = "TestPhotoURL",
                    CreatedAt = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    DocumentType = 0,
                    DocumentSeries = "string",
                    DocumentNumber = "string",
                    DateOfIssue = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    DateOfExpiry = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    PersonalNumber = 123456789,
                    IssuingAgency = "string",
                    ContactType = 0,
                    ContactInformation = "string",
                    AddressType = 0,
                    Address = "string",
                    RecommendedById = null,
                    UserName = "123456789",
                    NormalizedUserName = "123456789",
                    Email = "asdasd@gmail.com",
                    NormalizedEmail = "ASDASD@GMAIL.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEHX98Wf2M0minNuWUIGWiGfGc8UJxlGVyY9dNgwPBWSQ+m+fxMN8sZ9dWMCTXS4/pw==",
                    SecurityStamp = "BLCE7NYFV57PHMJIOKUNNAQBWCHKX4Q4",
                    ConcurrencyStamp = "7bfba293-b4c6-4bca-9f6e-4b55d86d8056",
                    PhoneNumber = "123123123",
                    AccummulatedBonus = 0,
                };
                context.Add(users);

                users = new UserEntity
                {
                    Id = Guid.Parse("3A4BB9D7-0E66-42CE-D7EA-08DC07AEA794"),
                    FirstName = "Alex",
                    Surname = "Smith",
                    DateOfBirth = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    Gender = 0,
                    PhotoURL = "TestPhotoURL",
                    CreatedAt = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    DocumentType = 0,
                    DocumentSeries = "string",
                    DocumentNumber = "string",
                    DateOfIssue = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    DateOfExpiry = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    PersonalNumber = 234567,
                    IssuingAgency = "string",
                    ContactType = 0,
                    ContactInformation = "string",
                    AddressType = 0,
                    Address = "string",
                    RecommendedById = Guid.Parse("DD089B36-4F62-4ACF-D021-08DC07AE017F"),
                    UserName = "234567",
                    NormalizedUserName = "234567",
                    Email = "sdasda@gmail.com",
                    NormalizedEmail = "SDASDA@GMAIL.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEHX98Wf2M0minNuWUIGWiGfGc8UJxlGVyY9dNgwPBWSQ+m+fxMN8sZ9dWMCTXS4/pw==",
                    SecurityStamp = "BLCE7NYFV57PHMJIOKUNNAQBWCHKX4Q4",
                    ConcurrencyStamp = "7bfba293-b4c6-4bca-9f6e-4b55d86d8056",
                    PhoneNumber = "567345",
                    AccummulatedBonus = 0,
                };
                context.Add(users);

                users = new UserEntity
                {
                    Id = Guid.Parse("35F4D385-9C49-49B9-D7EC-08DC07AEA794"),
                    FirstName = "George",
                    Surname = "Green",
                    DateOfBirth = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    Gender = 0,
                    PhotoURL = "TestPhotoURL",
                    CreatedAt = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    DocumentType = 0,
                    DocumentSeries = "string",
                    DocumentNumber = "string",
                    DateOfIssue = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    DateOfExpiry = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    PersonalNumber = 999999,
                    IssuingAgency = "string",
                    ContactType = 0,
                    ContactInformation = "string",
                    AddressType = 0,
                    Address = "string",
                    RecommendedById = Guid.Parse("DD089B36-4F62-4ACF-D021-08DC07AE017F"),
                    UserName = "999999",
                    NormalizedUserName = "999999",
                    Email = "qweqwe@gmail.com",
                    NormalizedEmail = "QWEQWE@GMAIL.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEHX98Wf2M0minNuWUIGWiGfGc8UJxlGVyY9dNgwPBWSQ+m+fxMN8sZ9dWMCTXS4/pw==",
                    SecurityStamp = "BLCE7NYFV57PHMJIOKUNNAQBWCHKX4Q4",
                    ConcurrencyStamp = "7bfba293-b4c6-4bca-9f6e-4b55d86d8056",
                    PhoneNumber = "34523452",
                    AccummulatedBonus = 0,
                };
                context.Add(users);

                users = new UserEntity
                {
                    Id = Guid.Parse("31494655-60B8-429B-D7ED-08DC07AEA794"),
                    FirstName = "Nickolas",
                    Surname = "Tesla",
                    DateOfBirth = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    Gender = 0,
                    PhotoURL = "TestPhotoURL",
                    CreatedAt = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    DocumentType = 0,
                    DocumentSeries = "string",
                    DocumentNumber = "string",
                    DateOfIssue = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    DateOfExpiry = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    PersonalNumber = 888888,
                    IssuingAgency = "string",
                    ContactType = 0,
                    ContactInformation = "string",
                    AddressType = 0,
                    Address = "string",
                    RecommendedById = Guid.Parse("35F4D385-9C49-49B9-D7EC-08DC07AEA794"),
                    UserName = "888888",
                    NormalizedUserName = "888888",
                    Email = "rtyrty@gmail.com",
                    NormalizedEmail = "RTYRTY@GMAIL.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEHX98Wf2M0minNuWUIGWiGfGc8UJxlGVyY9dNgwPBWSQ+m+fxMN8sZ9dWMCTXS4/pw==",
                    SecurityStamp = "BLCE7NYFV57PHMJIOKUNNAQBWCHKX4Q4",
                    ConcurrencyStamp = "7bfba293-b4c6-4bca-9f6e-4b55d86d8056",
                    PhoneNumber = "345234352",
                    AccummulatedBonus = 0,
                };
                context.Add(users);

                users = new UserEntity
                {
                    Id = Guid.Parse("F4BC66BB-F009-4F18-D7EF-08DC07AEA794"),
                    FirstName = "Kote",
                    Surname = "Grey",
                    DateOfBirth = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    Gender = 0,
                    PhotoURL = "TestPhotoURL",
                    CreatedAt = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    DocumentType = 0,
                    DocumentSeries = "string",
                    DocumentNumber = "string",
                    DateOfIssue = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    DateOfExpiry = DateTimeOffset.Parse("2023-12-28 13:50:48.7480000 +00:00"),
                    PersonalNumber = 777777,
                    IssuingAgency = "string",
                    ContactType = 0,
                    ContactInformation = "string",
                    AddressType = 0,
                    Address = "string",
                    RecommendedById = Guid.Parse("35F4D385-9C49-49B9-D7EC-08DC07AEA794"),
                    UserName = "777777",
                    NormalizedUserName = "777777",
                    Email = "cvbcvb@gmail.com",
                    NormalizedEmail = "CVBCVB@GMAIL.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEHX98Wf2M0minNuWUIGWiGfGc8UJxlGVyY9dNgwPBWSQ+m+fxMN8sZ9dWMCTXS4/pw==",
                    SecurityStamp = "BLCE7NYFV57PHMJIOKUNNAQBWCHKX4Q4",
                    ConcurrencyStamp = "7bfba293-b4c6-4bca-9f6e-4b55d86d8056",
                    PhoneNumber = "345234352",
                    AccummulatedBonus = 0,
                };
                context.Add(users);

                context.SaveChanges();
            }
        }

        private void SeedUserRoles(PashaBankDbContext context)
        {
            if (!context.UserRoles.Any())
            {
                UserRoleEntity userRole = new();
                userRole = new UserRoleEntity
                {
                    UserId = Guid.Parse("DD089B36-4F62-4ACF-D021-08DC07AE017F"),
                    RoleId = RoleConstants.DistributorRole
                };
                context.Add(userRole);

                userRole = new UserRoleEntity
                {
                    UserId = Guid.Parse("3A4BB9D7-0E66-42CE-D7EA-08DC07AEA794"),
                    RoleId = RoleConstants.DistributorRole
                };
                context.Add(userRole);

                userRole = new UserRoleEntity
                {
                    UserId = Guid.Parse("35F4D385-9C49-49B9-D7EC-08DC07AEA794"),
                    RoleId = RoleConstants.DistributorRole
                };
                context.Add(userRole);

                userRole = new UserRoleEntity
                {
                    UserId = Guid.Parse("31494655-60B8-429B-D7ED-08DC07AEA794"),
                    RoleId = RoleConstants.DistributorRole
                };
                context.Add(userRole);

                userRole = new UserRoleEntity
                {
                    UserId = Guid.Parse("F4BC66BB-F009-4F18-D7EF-08DC07AEA794"),
                    RoleId = RoleConstants.DistributorRole
                };
                context.Add(userRole);
                context.SaveChanges();
            }
        }

        private void SeedProductSales(PashaBankDbContext context)
        {
            if (!context.ProductSales.Any())
            {
                ProductSalesEntity productSales = new ProductSalesEntity();

                productSales = new ProductSalesEntity
                {
                    Id = Guid.Parse("DF9CA11C-7795-4962-94B4-08DC07D81215"),
                    UserId = Guid.Parse("DD089B36-4F62-4ACF-D021-08DC07AE017F"),
                    DateOfSale = DateTimeOffset.Parse("2023-12-25 13:50:48.7480000 +00:00"),
                    ProductId = Guid.Parse("4edf7903-ee5f-4b74-ba56-d8e579771f2a"),
                    TotalPrice = 25,
                    CreatedAt = DateTimeOffset.Parse("2023-12-28 23:06:17.7132775 +00:00"),
                    SellPrice = 15,
                    WasCalculated = false
                };
                context.Add(productSales);

                productSales = new ProductSalesEntity
                {
                    Id = Guid.Parse("BBC5B042-701C-419C-94B6-08DC07D81215"),
                    UserId = Guid.Parse("3A4BB9D7-0E66-42CE-D7EA-08DC07AEA794"),
                    DateOfSale = DateTimeOffset.Parse("2023-12-25 13:50:48.7480000 +00:00"),
                    ProductId = Guid.Parse("4edf7903-ee5f-4b74-ba56-d8e579771f2a"),
                    TotalPrice = 25,
                    CreatedAt = DateTimeOffset.Parse("2023-12-28 23:06:17.7132775 +00:00"),
                    SellPrice = 15,
                    WasCalculated = false
                };
                context.Add(productSales);

                productSales = new ProductSalesEntity
                {
                    Id = Guid.Parse("23FC3EBB-1D1A-4DBF-94B8-08DC07D81215"),
                    UserId = Guid.Parse("35F4D385-9C49-49B9-D7EC-08DC07AEA794"),
                    DateOfSale = DateTimeOffset.Parse("2023-12-25 13:50:48.7480000 +00:00"),
                    ProductId = Guid.Parse("4edf7903-ee5f-4b74-ba56-d8e579771f2a"),
                    TotalPrice = 25,
                    CreatedAt = DateTimeOffset.Parse("2023-12-28 23:06:17.7132775 +00:00"),
                    SellPrice = 15,
                    WasCalculated = false
                };
                context.Add(productSales);

                productSales = new ProductSalesEntity
                {
                    Id = Guid.Parse("EB1EF085-6DA6-4066-94B9-08DC07D81215"),
                    UserId = Guid.Parse("31494655-60B8-429B-D7ED-08DC07AEA794"),
                    DateOfSale = DateTimeOffset.Parse("2023-12-25 13:50:48.7480000 +00:00"),
                    ProductId = Guid.Parse("4edf7903-ee5f-4b74-ba56-d8e579771f2a"),
                    TotalPrice = 25,
                    CreatedAt = DateTimeOffset.Parse("2023-12-28 23:06:17.7132775 +00:00"),
                    SellPrice = 15,
                    WasCalculated = false
                };
                context.Add(productSales);

                productSales = new ProductSalesEntity
                {
                    Id = Guid.Parse("9797A9A8-51EC-4DA4-94BA-08DC07D81215"),
                    UserId = Guid.Parse("F4BC66BB-F009-4F18-D7EF-08DC07AEA794"),
                    DateOfSale = DateTimeOffset.Parse("2023-12-25 13:50:48.7480000 +00:00"),
                    ProductId = Guid.Parse("4edf7903-ee5f-4b74-ba56-d8e579771f2a"),
                    TotalPrice = 25,
                    CreatedAt = DateTimeOffset.Parse("2023-12-28 23:06:17.7132775 +00:00"),
                    SellPrice = 15,
                    WasCalculated = false
                };
                context.Add(productSales);
                context.SaveChanges();
            }
        }

        #endregion
    }
}
