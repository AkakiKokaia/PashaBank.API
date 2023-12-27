using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PashaBank.Domain.Entities;

namespace PashaBank.Infrastructure.EntityConfigurations.RefreshToken
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(rt => rt.RefreshToken);

            builder.Property(rt => rt.RefreshToken)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(rt => rt.UserId)
                .IsRequired();

            builder.Property(rt => rt.ExpirationTime)
                .IsRequired();

            builder
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
