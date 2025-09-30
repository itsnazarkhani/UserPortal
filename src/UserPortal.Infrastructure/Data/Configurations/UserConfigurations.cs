using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserPortal.Core.Constants;
using UserPortal.Core.Entities;

namespace UserPortal.Infrastructure.Data.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Primary key
        builder.HasKey(u => u.Id);

        // Rules
        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(ValidationConstants.UserName.MaxLength);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired();

        // Indexes
        builder.HasIndex(u => u.UserName);
        builder.HasIndex(u => u.Email);
        builder.HasIndex(u => u.FirstName);
        builder.HasIndex(u => u.LastName);
    }
}
