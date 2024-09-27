using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.FatherName)
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .HasMaxLength(250)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(18)
            .IsRequired();

        builder.HasIndex(x => x.PhoneNumber)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRoleEntity>(
                r => r.HasOne<RoleEntity>().WithMany().HasForeignKey(ur => ur.RoleId),
                u => u.HasOne<UserEntity>().WithMany().HasForeignKey(ur => ur.UserId)
            );

        builder.HasOne(x => x.Address)
            .WithOne(x => x.User)
            .HasForeignKey<UserEntity>(x => x.AddressId);

        builder.HasOne(x => x.Image)
            .WithOne(x => x.User)
            .HasForeignKey<UserEntity>(x => x.ImageId);
    }
}
