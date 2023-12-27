using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PashaBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace PashaBank.Infrastructure.EntityConfigurations.User
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder
                .HasMany(r => r.Users)
                .WithOne(r => r.Role)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
