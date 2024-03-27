﻿using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string? RefreshRoken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }



}

