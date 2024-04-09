using System;
using MediatR;
using System.ComponentModel;
using Application.Features.Auth.Rules;
using Application.Interfaces.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoginedCommandResponse>, ITransactionalRequest,
    ILoggableRequest
{
    [DefaultValue("hasankaya@gmail.com")] public string Email { get; set; }
    [DefaultValue("123321")] public string Password { get; set; }


    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginedCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly AuthRules _authRules;

        public LoginCommandHandler(UserManager<AppUser> userManager, IConfiguration configuration,
            ITokenService tokenService, AuthRules authRules)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenService = tokenService;
            _authRules = authRules;
        }

        public async Task<LoginedCommandResponse> Handle(LoginCommand request,
            CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByEmailAsync(request.Email);

            await _authRules.UserDontExists(user);

            bool checkPassword = await _userManager.CheckPasswordAsync(user!, request.Password);

            await _authRules.EmailOrPasswordShouldNotBeInvalid(user, checkPassword);

            IList<string> roles = await _userManager.GetRolesAsync(user);

            JwtSecurityToken token = await _tokenService.CreateToken(user, roles);
            string refreshToken = _tokenService.CreateRefreshToken();

            _ = int.TryParse(_configuration["TokenOptions:RefreshTokenTTL"],
                out int refreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);

            string _token = new JwtSecurityTokenHandler().WriteToken(token);

            await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);

            return new()
            {
                AccessToken = _token,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };
        }
    }
}