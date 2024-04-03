using System;
namespace Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenResponse
{

    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

