using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUserToken : IdentityUserToken<Guid>
{
    public AppUserToken()
    {
    }
}

