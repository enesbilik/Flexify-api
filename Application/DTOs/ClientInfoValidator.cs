using Application.Enums;
using FluentValidation;

namespace Application.DTOs;

public class ClientInfoValidator : AbstractValidator<ClientInfoDto>
{
    public ClientInfoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(60)
            .WithName("İsim")
            .WithMessage("İsim alanı boş bırakılamaz");

        RuleFor(x => x.SurName)
            .NotEmpty()
            .MaximumLength(60)
            .WithName("Soyisim")
            .WithMessage("Soyisim alanı boş bırakılamaz");
    }
}