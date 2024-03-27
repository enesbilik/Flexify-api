using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUserRole : IdentityUserRole<Guid>
{
    public AppUserRole()
    {
    }
}

