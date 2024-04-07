using Application.Enums;
using FluentValidation;

namespace Application.DTOs;

public class ConsultantInfoValidator : AbstractValidator<ConsultantInfoDto>
{
    public ConsultantInfoValidator()
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
        RuleFor(x => x.PhotoUrl)
            .NotEmpty()
            .MaximumLength(200)
            .WithName("Fotoğraf URL")
            .WithMessage("Fotoğraf alanı boş bırakılamaz");

        RuleFor(x => x.About)
            .NotEmpty()
            .MaximumLength(400)
            .WithName("Hakkımda")
            .WithMessage("Hakkımda alanı boş bırakılamaz");

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(200)
            .WithName("Konum")
            .WithMessage("Konum alanı boş bırakılamaz");

        RuleFor(x => x.Experience)
            .NotEmpty()
            .WithName("Deneyim")
            .WithMessage("Deneyim alanı boş bırakılamaz");

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200)
            .WithName("Ünvan")
            .WithMessage("Ünvan alanı boş bırakılamaz");
    }
}