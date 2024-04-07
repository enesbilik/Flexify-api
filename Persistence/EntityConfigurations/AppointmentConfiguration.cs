using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments").HasKey(a => a.Id);

        builder.Property(a => a.StartTime)
            .HasColumnName("StartTime").IsRequired();

        builder.Property(a => a.EndTime)
            .HasColumnName("EndTime").IsRequired();

        builder.Property(a => a.Status)
            .HasColumnName("State").IsRequired();

        builder.HasOne(a => a.Client)
            .WithMany(c => c.Appointments)
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Consultant)
            .WithMany(c => c.Appointments)
            .HasForeignKey(r => r.ConsultantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}