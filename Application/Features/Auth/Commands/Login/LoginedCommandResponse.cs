using System;
namespace Application.Features.Auth.Commands.Login;

public class LoginedCommandResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}

