using Clinic.DataAccess.Models;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.DataAccess.Configurations;

public class ResultICDConfiguration : IEntityTypeConfiguration<ResultICDEntity>
{
    public void Configure(EntityTypeBuilder<ResultICDEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ICDCode)
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(250);

        builder.HasOne(x => x.Reception)
            .WithMany(x => x.Results)
            .HasForeignKey(x => x.ReceptionId);
    }
}
