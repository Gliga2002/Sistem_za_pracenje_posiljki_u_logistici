using FluentValidation;
using Backend.Models;

namespace Backend.Validators
{
    public class PosiljkaValidator : AbstractValidator<Posiljka>
    {
        public PosiljkaValidator()
        {
            RuleFor(p => p.Naziv).NotEmpty().WithMessage("Naziv je obavezan.");
            RuleFor(p => p.Status).IsInEnum().WithMessage("Status mora biti validan.");
            RuleFor(p => p.DatumIsporuke)
                .GreaterThan(DateTime.Now).When(p => p.DatumIsporuke.HasValue)
                .WithMessage("Datum isporuke mora biti u budućnosti.");
        }
    }
}