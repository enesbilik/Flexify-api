using System;
using System.Drawing.Drawing2D;
using Application.Features.Auth.Rules;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;
using static StackExchange.Redis.Role;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredCommandResponse>, ILoggableRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }



    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredCommandResponse>
    {
        private readonly AuthRules _authRules;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(AuthRules authRules, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _authRules = authRules;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<RegisteredCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authRules.UserMailAlreadyExists(await _userManager.FindByEmailAsync(request.Email));

            AppUser user = _mapper.Map<AppUser>(request);
            user.UserName = request.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("user"))
                    await _roleManager.CreateAsync(new AppRole
                    {
                        Id = Guid.NewGuid(),
                        Name = "user",
                        NormalizedName = "USER",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    });

                await _userManager.AddToRoleAsync(user, "user");
            }

            RegisteredCommandResponse registeredCommandResponse = _mapper.Map<RegisteredCommandResponse>(user);
            return registeredCommandResponse;
        }

    }
}

