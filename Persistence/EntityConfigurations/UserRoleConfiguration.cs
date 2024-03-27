using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        // Primary key
        builder.HasKey(r => new { r.UserId, r.RoleId });

        // Maps to the AspNetUserRoles table
        builder.ToTable("AspNetUserRoles");


        builder.HasData(new AppUserRole
        {
            UserId = Guid.Parse("9f81f2ca-0dc5-4bb6-b8ea-e60f296b5231"),
            RoleId = Guid.Parse("682bd438-dac9-485e-9eea-d1e506f96ae6")
        },
        new AppUserRole
        {
            UserId = Guid.Parse("cf6db848-b71b-4bb9-b37a-bd6e11f90f60"),
            RoleId = Guid.Parse("a4ad46b1-5cba-46fa-a804-0b81773b8ff0")
        }

        );
    }
}

