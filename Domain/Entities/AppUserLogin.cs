using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUserLogin : IdentityUserLogin<Guid>
{
    public AppUserLogin()
    {
    }
}

