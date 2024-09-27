using Clinic.Core.Enums;
using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.DataAccess.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<PositionEntity>
{
    public void Configure(EntityTypeBuilder<PositionEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        var positions = Enum.GetValues<PositionEmployee>()
            .Select(p => new PositionEntity
            {
                Id = (int)p,
                Name = p.ToString()
            });

        builder.HasData(positions);

        builder.Property(x => x.Description)
            .HasMaxLength(255);
    }
}
