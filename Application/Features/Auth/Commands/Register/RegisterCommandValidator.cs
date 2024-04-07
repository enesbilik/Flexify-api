using System;
using Application.DTOs;
using Application.Enums;
using FluentValidation;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(60)
            .EmailAddress()
            .MinimumLength(8)
            .WithName("E-posta Adresi");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .WithName("Parola");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .MinimumLength(6)
            .Equal(x => x.Password)
            .WithName("Parola Tekrarı");

        RuleFor(x => x.UserType)
            .IsInEnum()
            .WithName("Kullanıcı Tipi Hatalı");


        When(x => x.UserType == UserType.Consultant, () =>
        {
            RuleFor(x => x.ConsultantInfoDto)
                .SetValidator(new ConsultantInfoValidator());
        });

        When(x => x.UserType == UserType.Client, () =>
        {
            RuleFor(x => x.ClientInfoDto)
                .SetValidator(new ClientInfoValidator());
        });
    }
}