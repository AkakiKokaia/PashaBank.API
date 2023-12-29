using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces;

namespace PashaBank.Infrastructure
{
    public class PashaBankDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid, IdentityUserClaim<Guid>, UserRoleEntity, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, ITransactionBehaviour
    {
        protected readonly IConfiguration Configuration;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public PashaBankDbContext(DbContextOptions<PashaBankDbContext> options, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMediator mediator)
            : base(options)
        {
            Configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Role { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductSalesEntity> ProductSales { get; set; }

        #region Transaction
        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            _currentTransaction ??= await Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                await SaveChangesAsync(cancellationToken);
                if (_currentTransaction != null) await _currentTransaction.CommitAsync(cancellationToken);
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Database.CreateExecutionStrategy();
        }

        public async Task RollbackTransaction()
        {
            try
            {
                await _currentTransaction?.RollbackAsync()!;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }


        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRoleEntity>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(u => u.UserId);

            builder.Entity<UserRoleEntity>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(ur => ur.RoleId);

            builder.Entity<UserEntity>().Property(x => x.AccummulatedBonus).HasDefaultValue(0);
        }
    }
}