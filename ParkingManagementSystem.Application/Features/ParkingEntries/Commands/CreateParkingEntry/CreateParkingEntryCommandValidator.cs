using FluentValidation;

namespace ParkingManagementSystem.Application.Features.ParkingEntries.Commands.CreateParkingEntry
{
    public class CreateParkingEntryCommandValidator : AbstractValidator<CreateParkingEntryCommand>
    {
        public CreateParkingEntryCommandValidator()
        {
            RuleFor(x => x.LicensePlate)
                .NotEmpty().WithMessage("El número de placa es requerido.")
                .MaximumLength(7).WithMessage("El número de placa no puede exceder los 7 caracteres.")
                .Matches("^[A-Z0-9]{3}-\\d{3}$")
                .WithMessage("El número de placa debe tener un formato válido. Ejemplo: ABC123 o ABC-123.");

            RuleFor(x => x.RateTypeId)
                .GreaterThan(0).WithMessage("La tarifa debe ser mayor a 0.");
        }
    }
}