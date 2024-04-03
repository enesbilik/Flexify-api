using System;
using FluentValidation;

namespace Application.Features.Auth.Commands.Revoke;

public class RevokeCommandValidator : AbstractValidator<RevokeCommand>
{
    public RevokeCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();
    }
}

