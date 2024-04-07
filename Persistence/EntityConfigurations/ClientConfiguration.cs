using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients").HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasColumnName("Name").IsRequired();

        builder.Property(c => c.SurName)
            .HasColumnName("SurName").IsRequired();

        builder.Property(c => c.Email)
            .HasColumnName("Email").IsRequired();
        
        builder.HasMany(c => c.Reviews)
            .WithOne(r => r.Client)
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.Appointments)
            .WithOne(a => a.Client)
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasQueryFilter(c => !c.DeletedDate.HasValue);
    }
}