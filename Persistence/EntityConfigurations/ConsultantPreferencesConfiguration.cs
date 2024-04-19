using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ConsultantPreferencesConfiguration : IEntityTypeConfiguration<ConsultantPreferences>
{
    public void Configure(EntityTypeBuilder<ConsultantPreferences> builder)
    {
        builder.ToTable("ConsultantPreferences").HasKey(cp => cp.ConsultantId);

        builder.Property(cp => cp.ConsultantId)
            .HasColumnName("ConsultantId").IsRequired();


        builder.Property(cp => cp.StartTime)
            .HasColumnName("StartTime").IsRequired();

        builder.Property(cp => cp.EndTime)
            .HasColumnName("EndTime").IsRequired();

        builder.Property(cp => cp.AppointmentInterval)
            .HasColumnName("AppointmentInterval").IsRequired();

        builder.Property(cp => cp.LunchBreakStartTime)
            .HasColumnName("LunchBreakStartTime").IsRequired();

        builder.Property(cp => cp.LunchBreakEndTime)
            .HasColumnName("LunchBreakEndTime").IsRequired();

        builder.Property(cp => cp.DaysAheadForAppointment)
            .HasColumnName("DaysAheadForAppointment").IsRequired();

        builder.Property(cp => cp.IsWeekendAppointmentAllowed)
            .HasColumnName("IsWeekendAppointmentAllowed").IsRequired();

        builder.HasOne(cp => cp.Consultant)
            .WithOne(c => c.ConsultantPreferences)
            .HasForeignKey<ConsultantPreferences>(cp => cp.ConsultantId);
    }
}