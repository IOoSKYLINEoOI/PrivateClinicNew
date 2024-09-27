using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.DataAccess.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Country)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Region)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.City)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Street)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.HouseNumber)
            .IsRequired();

        builder.Property(x => x.Pavilion)
            .HasMaxLength(100);

        builder.Property(x => x.ApartmentNumber)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(255);
    }
}