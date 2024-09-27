using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.DataAccess.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<AppointmentEntity>
{
    public void Configure(EntityTypeBuilder<AppointmentEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.HasOne(x => x.User)
           .WithMany(x => x.Appointments)
           .HasForeignKey(x => x.UserId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Reception)
            .WithMany(x => x.Appointments)
            .HasForeignKey(x => x.ReceptionId);

        builder.Property(x => x.TimeSlotId)
            .IsRequired();

        builder.HasOne(x => x.TimeSlot)
            .WithOne(x => x.Appointment)
            .HasForeignKey<AppointmentEntity>(x => x.TimeSlotId);

        builder.Property(x => x.DateOfBooking)
            .IsRequired();

        builder.Property(x => x.StatusAppointmentId)
            .IsRequired();
    }

}
