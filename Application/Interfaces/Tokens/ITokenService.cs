using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Entities;

namespace Application.Interfaces.Tokens;

public interface ITokenService
{
    Task<JwtSecurityToken> CreateToken(AppUser user, IList<string> roles);

    string CreateRefreshToken();

    ClaimsPrincipal? GetPrinipalFromExpiredToken();
}

