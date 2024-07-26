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
                .HasMaxLength(250);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Receptions)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Department)
                .WithMany(x => x.Receptions)
                .HasForeignKey(x => x.DepartmentId);

            builder.HasOne(x => x.Employee)
                .WithMany(x => x.Receptions)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
