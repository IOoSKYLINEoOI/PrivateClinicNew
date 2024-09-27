using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.DataAccess.Configurations;

public class ReceptionConfiguration : IEntityTypeConfiguration<ReceptionEntity>
{
    public void Configure(EntityTypeBuilder<ReceptionEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DateReceipt)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(255);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.DepartmentId)
            .IsRequired();

        builder.Property(x => x.EmployeeId)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Receptions)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Department)
            .WithMany(x => x.Receptions)
            .HasForeignKey(x => x.DepartmentId);

        builder.HasOne(x => x.Employee)
            .WithMany(x => x.Receptions)
            .HasForeignKey(x => x.EmployeeId);
    }
}
