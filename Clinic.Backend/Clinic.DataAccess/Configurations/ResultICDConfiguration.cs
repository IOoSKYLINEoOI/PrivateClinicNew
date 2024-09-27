using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.DataAccess.Configurations;

public class ResultICDConfiguration : IEntityTypeConfiguration<ResultICDEntity>
{
    public void Configure(EntityTypeBuilder<ResultICDEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ICDCode)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(255);

        builder.Property(x => x.ReceptionId)
            .IsRequired();

        builder.HasOne(x => x.Reception)
            .WithMany(x => x.Results)
            .HasForeignKey(x => x.ReceptionId);
    }
}
