using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Clinic.DataAccess.Configurations;
public class EmployeeDepartmentConfiguration : IEntityTypeConfiguration<EmployeeDepartmentEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeDepartmentEntity> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.DepartmentId });

        builder.Property(x => x.Description)
            .HasMaxLength(250);

        builder.HasOne(x => x.Position)
            .WithMany(p => p.EmployeeDepartments)
            .HasForeignKey(x => x.PositionId);
    }
}
