using System;
using Core.Security.Hashing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;


public class UserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("Users").HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnName("Id").IsRequired();
        builder.Property(u => u.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(u => u.LastName).HasColumnName("LastName").IsRequired();
        builder.Property(u => u.Email).HasColumnName("Email").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);

        builder.HasData(getSeeds());
    }

    private IEnumerable<AppUser> getSeeds()
    {
        List<AppUser> users = new();

        HashingHelper.CreatePasswordHash(
            password: "EnesBey",
            passwordHash: out byte[] passwordHash,
            passwordSalt: out byte[] passwordSalt
        );
        AppUser adminUser =
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Enes",
                LastName = "Bilik",
                Email = "admin@enes.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
        users.Add(adminUser);

        return users.ToArray();
    }
}