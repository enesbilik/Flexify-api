using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.RefreshToken;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.Revoke;
using Application.Features.Auth.Commands.RevokeAll;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    {
        RegisteredCommandResponse registeredCommandResponse = await Mediator.Send(request);
        return StatusCode(StatusCodes.Status201Created, registeredCommandResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand request)
    {
        LoginedCommandResponse response = await Mediator.Send(request);
        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpPost]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand request)
    {
        var response = await Mediator.Send(request);
        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpPost]
    public async Task<IActionResult> Revoke(RevokeCommand request)
    {
        await Mediator.Send(request);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpPost]
    public async Task<IActionResult> RevokeAll()
    {
        await Mediator.Send(new RevokeAllCommand());
        return StatusCode(StatusCodes.Status200OK);
    }
}