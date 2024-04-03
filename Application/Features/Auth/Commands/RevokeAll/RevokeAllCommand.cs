using System;
using AutoMapper;
using Core.Application.Bases;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands.RevokeAll;

public class RevokeAllCommand : IRequest<Unit>
{

    public class RevokeAllCommandHandler : IRequestHandler<RevokeAllCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;

        public RevokeAllCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(RevokeAllCommand request, CancellationToken cancellationToken)
        {
            List<AppUser> users = await _userManager.Users.ToListAsync(cancellationToken);

            foreach (AppUser user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return Unit.Value;
        }
    }
}

