using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PashaBank.Domain.Entities;
using PashaBank.Domain.Entities.Common;
using System.Security.Claims;

namespace PashaBank.Infrastucture
{
    public class PashaBankDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        protected readonly IConfiguration Configuration;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;

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
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

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

        }
    }
}