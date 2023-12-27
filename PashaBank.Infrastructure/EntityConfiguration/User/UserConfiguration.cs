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
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder
                .HasMany(u => u.Roles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(rt => rt.RefreshTokens)
                .WithOne(u => u.User)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.Surname).HasMaxLength(50);
            builder.Property(x => x.DocumentSeries).HasMaxLength(10);
            builder.Property(x => x.DocumentNumber).HasMaxLength(10);
            builder.Property(x => x.PersonalNumber).HasMaxLength(50);
            builder.Property(x => x.IssuingAgency).HasMaxLength(100);
            builder.Property(x => x.ContactInformation).HasMaxLength(100);
            builder.Property(x => x.Address).HasMaxLength(100);
        }
    }
}
