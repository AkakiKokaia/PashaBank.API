using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PashaBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashaBank.Infrastructure.EntityConfigurations.UserRoles
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder
                .HasOne(ur => ur.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(ur => ur.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
