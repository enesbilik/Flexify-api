using System;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<AppUser>
{

    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(u => u.Id);

        // Indexes for "normalized" username and email, to allow efficient lookups
        builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

        // Maps to the AspNetUsers table
        builder.ToTable("AspNetUsers");

        // A concurrency token for use with the optimistic concurrency checking
        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

        // Limit the size of columns to use efficient database types
        builder.Property(u => u.UserName).HasMaxLength(256);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
        builder.Property(u => u.Email).HasMaxLength(256);
        builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

        // The relationships between User and other entity types
        // Note that these relationships are configured with no navigation properties

        // Each User can have many UserClaims
        builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

        // Each User can have many UserLogins
        builder.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

        // Each User can have many UserTokens
        builder.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

        // Each User can have many entries in the UserRole join table
        builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

        var superAdmin = new AppUser
        {
            Id = Guid.Parse("9f81f2ca-0dc5-4bb6-b8ea-e60f296b5231"),
            UserName = "superadmin@gmail.com",
            Email = "superadmin@gmail.com",
            NormalizedEmail = "SUPERADMIN@GMAIL.COM",
            PhoneNumber = "+905442563413",
            FirstName = "Enes",
            LastName = "Bilik",
            PhoneNumberConfirmed = true,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        superAdmin.PasswordHash = CreatePasswordHash(superAdmin, "123456");

        var admin = new AppUser
        {
            Id = Guid.Parse("cf6db848-b71b-4bb9-b37a-bd6e11f90f60"),
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            PhoneNumber = "+905385438863",
            FirstName = "Beyza",
            LastName = "Kutsal",
            PhoneNumberConfirmed = false,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        admin.PasswordHash = CreatePasswordHash(admin, "1234567");

        builder.HasData(superAdmin, admin);

    }


    private string CreatePasswordHash(AppUser user, string password)
    {
        var passwordHasher = new PasswordHasher<AppUser>();
        return passwordHasher.HashPassword(user, password);
    }
}

