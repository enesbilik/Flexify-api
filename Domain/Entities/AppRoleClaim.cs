using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppRoleClaim : IdentityRoleClaim<Guid>
{
    public AppRoleClaim()
    {
    }
}

