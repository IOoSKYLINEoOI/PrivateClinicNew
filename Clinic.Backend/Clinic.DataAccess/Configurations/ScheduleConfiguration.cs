using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.DataAccess.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<ScheduleEntity>
{
    public void Configure(EntityTypeBuilder<ScheduleEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.EmployeeId)
             .IsRequired();

        builder.HasOne(x => x.Employee)
           .WithMany(x => x.Schedules)
           .HasForeignKey(x => x.EmployeeId);

        builder.Property(x => x.WorkDate)
             .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();
    }
}
