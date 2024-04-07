using System;
using Core.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser<Guid>
{

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }



}

