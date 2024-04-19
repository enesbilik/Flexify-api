using System;
using System.Drawing.Drawing2D;
using Application.DTOs;
using Application.Enums;
using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;
using static StackExchange.Redis.Role;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredCommandResponse>, ILoggableRequest,
    ITransactionalRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public UserType UserType { get; set; } = UserType.None; // Default value

    public ConsultantInfoDto? ConsultantInfoDto { get; set; } =
        new ConsultantInfoDto(); // Default value

    public ClientInfoDto? ClientInfoDto { get; set; } = new ClientInfoDto(); // Default value


    public class
        RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredCommandResponse>
    {
        private readonly AuthRules _authRules;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConsultantRepository _consultantRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IConsultantPreferencesRepository _consultantPreferencesRepository;

        public RegisterCommandHandler(AuthRules authRules, UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager, IMapper mapper,
            IConsultantRepository consultantRepository, IClientRepository clientRepository,
            IConsultantPreferencesRepository consultantPreferencesRepository)
        {
            _authRules = authRules;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _consultantRepository = consultantRepository;
            _clientRepository = clientRepository;
            _consultantPreferencesRepository = consultantPreferencesRepository;
        }

        public async Task<RegisteredCommandResponse> Handle(RegisterCommand request,
            CancellationToken cancellationToken)
        {
            await _authRules.UserMailAlreadyExists(
                await _userManager.FindByEmailAsync(request.Email));

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

                if (request.UserType == UserType.Client)
                {
                    var client = _mapper.Map<Client>(request.ClientInfoDto);
                    client.Email = request.Email;
                    await _clientRepository.AddAsync(client);
                }
                else if (request.UserType == UserType.Consultant)
                {
                    var consultant = _mapper.Map<Domain.Entities.Consultant>(request.ConsultantInfoDto);
                    consultant.Email = request.Email;

                    var consultantPreferences = new Domain.Entities.ConsultantPreferences(consultantId: consultant.Id);

                    await _consultantRepository.AddAsync(consultant);
                    await _consultantPreferencesRepository.AddAsync(consultantPreferences);
                }
            }

            RegisteredCommandResponse registeredCommandResponse =
                _mapper.Map<RegisteredCommandResponse>(user);
            return registeredCommandResponse;
        }
    }
}