using System;
using FluentValidation;

namespace Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.AccessToken).NotEmpty();

        RuleFor(x => x.RefreshToken).NotEmpty();
    }

}

