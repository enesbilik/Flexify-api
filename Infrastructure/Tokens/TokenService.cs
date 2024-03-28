using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces.Tokens;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Tokens;

public class TokenService : ITokenService
{

    private readonly UserManager<AppUser> _userManager;
    private readonly TokenOptions _tokenOptions;

    public TokenService(UserManager<AppUser> userManager, TokenOptions tokenOptions)
    {
        _userManager = userManager;
        _tokenOptions = tokenOptions;
    }

    public async Task<JwtSecurityToken> CreateToken(AppUser user, IList<string> roles)
    {
        var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

        var token = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            expires: DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration),
            claims: claims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

        await _userManager.AddClaimsAsync(user, claims);

        return token;
    }


    public string CreateRefreshToken()
    {
        throw new NotImplementedException();
    }



    public ClaimsPrincipal? GetPrinipalFromExpiredToken()
    {
        throw new NotImplementedException();
    }
}

