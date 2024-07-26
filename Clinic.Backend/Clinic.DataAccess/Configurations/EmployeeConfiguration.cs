using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.DataAccess.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.HiringDate)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(250);

        builder.HasOne(e => e.User)
            .WithOne(e => e.Employee)
            .HasForeignKey<EmployeeEntity>(e => e.UserId)
            .IsRequired();

        builder.HasMany(e => e.Departments)
            .WithMany(d => d.Employees)
            .UsingEntity<EmployeeDepartmentEntity>(
                e => e.HasOne<DepartmentEntity>().WithMany().HasForeignKey(ed => ed.DepartmentId),
                d => d.HasOne<EmployeeEntity>().WithMany().HasForeignKey(ed => ed.EmployeeId)
                    .OnDelete(DeleteBehavior.NoAction)
            );
    }
}
