using System;
using Application.Features.Auth.Rules;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.Revoke;

public class RevokeCommand : IRequest<Unit>, ITransactionalRequest, ILoggableRequest 
{
    public string Email { get; set; }


    public class RevokeCommandHandler : IRequestHandler<RevokeCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AuthRules _authRules;

        public RevokeCommandHandler(UserManager<AppUser> userManager, AuthRules authRules)
        {
            _userManager = userManager;
            _authRules = authRules;
        }

        public async Task<Unit> Handle(RevokeCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            await _authRules.UserDontExists(user);

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}