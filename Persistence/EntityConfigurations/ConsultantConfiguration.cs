using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ConsultantConfiguration : IEntityTypeConfiguration<Consultant>
{
    public void Configure(EntityTypeBuilder<Consultant> builder)
    {
        builder.ToTable("Consultants").HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasColumnName("Name").IsRequired();

        builder.Property(c => c.SurName)
            .HasColumnName("SurName").IsRequired();

        builder.Property(c => c.Email)
            .HasColumnName("Email").IsRequired();

        builder.Property(c => c.PhotoUrl)
            .HasColumnName("PhotoUrl").IsRequired();

        builder.Property(c => c.About)
            .HasColumnName("About").IsRequired();

        builder.Property(c => c.Location)
            .HasColumnName("Location").IsRequired();

        builder.Property(c => c.Experience)
            .HasColumnName("Experience").IsRequired();

        builder.Property(c => c.Title)
            .HasColumnName("Title").IsRequired();

        builder.Property(c => c.Rating)
            .HasColumnName("Rating").IsRequired();

        builder.Property(c => c.ServiceCount)
            .HasColumnName("ServiceCount").IsRequired();

        builder.HasMany(c => c.Reviews)
            .WithOne(r => r.Consultant)
            .HasForeignKey(r => r.ConsultantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Appointments)
            .WithOne(a => a.Consultant)
            .HasForeignKey(a => a.ConsultantId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasQueryFilter(c => !c.DeletedDate.HasValue);
    }
}