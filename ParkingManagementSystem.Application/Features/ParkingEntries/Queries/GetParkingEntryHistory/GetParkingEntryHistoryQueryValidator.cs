using FluentValidation;

namespace ParkingManagementSystem.Application.Features.ParkingEntries.Queries.GetParkingEntryHistory
{
    public class GetParkingEntryHistoryQueryValidator : AbstractValidator<GetParkingEntryHistoryQuery>
    {
        public GetParkingEntryHistoryQueryValidator()
        {
            RuleFor(x => x.LicensePlate)
                .MaximumLength(7)
                .WithMessage("El número de placa no puede exceder los 7 caracteres.")
                .Matches("^[A-Z0-9]{3}-\\d{3}$")
                .WithMessage("El número de placa debe tener un formato válido. Ejemplo: A1S-959 o ABC-123.")
                .When(x => !string.IsNullOrWhiteSpace(x.LicensePlate));

            RuleFor(x => x.From)
                .LessThanOrEqualTo(DateTime.Now)
                .When(x => x.From.HasValue)
                .WithMessage("La fecha de inicio no puede ser mayor a la fecha actual.");

            RuleFor(x => x.To)
                .LessThanOrEqualTo(DateTime.Now)
                .When(x => x.To.HasValue)
                .WithMessage("La fecha final no puede ser mayor a la fecha actual.");

            RuleFor(x => x.From)
                .LessThanOrEqualTo(x => x.To)
                .When(x => x.From.HasValue && x.To.HasValue)
                .WithMessage("La fecha de inicio no puede ser mayor que la fecha final.");

        }
    }
}
