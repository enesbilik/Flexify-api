using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUserClaim : IdentityUserClaim<Guid>
{
    public AppUserClaim()
    {
    }
}

