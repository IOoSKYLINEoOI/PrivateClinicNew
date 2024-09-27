using Clinic.Core.Enums;
using Clinic.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.DataAccess.Configurations;

public class StatusAppointmentConfiguration : IEntityTypeConfiguration<StatusAppointmentEntity>
{
    public void Configure(EntityTypeBuilder<StatusAppointmentEntity> builder)
    {
        builder.HasKey(x => x.Id);

        var statuses = Enum.GetValues<StatusAppointment>()
            .Select(p => new StatusAppointmentEntity
            {
                Id = (int)p,
                Name = p.ToString()
            });

        builder.HasData(statuses);
    }
}
