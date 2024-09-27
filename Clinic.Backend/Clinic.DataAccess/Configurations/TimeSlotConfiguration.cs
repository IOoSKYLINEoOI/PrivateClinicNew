using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.DataAccess.Configurations;

public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlotEntity>
{
    public void Configure(EntityTypeBuilder<TimeSlotEntity> builder)
    {
        builder.HasKey(x => x.Id);

        
        builder.HasOne(x => x.Schedule)
            .WithMany(x => x.TimeSlots) 
            .HasForeignKey(x => x.ScheduleId);

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.Property(x => x.IsAvailable)
            .HasDefaultValue(true)
            .IsRequired();
    }
}
