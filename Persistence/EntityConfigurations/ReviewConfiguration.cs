using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews").HasKey(r => r.Id);

        builder.Property(r => r.Rating)
            .HasColumnName("Rating").IsRequired();

        builder.Property(r => r.Comment)
            .HasColumnName("Comment").HasMaxLength(500);

        builder.HasOne(r => r.Client)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Consultant)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.ConsultantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}