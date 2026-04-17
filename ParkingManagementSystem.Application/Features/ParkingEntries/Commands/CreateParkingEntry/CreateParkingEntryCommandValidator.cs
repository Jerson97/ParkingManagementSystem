using FluentValidation;

namespace ParkingManagementSystem.Application.Features.ParkingEntries.Commands.CreateParkingEntry
{
    public class CreateParkingEntryCommandValidator : AbstractValidator<CreateParkingEntryCommand>
    {
        public CreateParkingEntryCommandValidator()
        {
            RuleFor(x => x.LicensePlate)
                .NotEmpty().WithMessage("El número de placa es requerido.")
                .MaximumLength(7).WithMessage("El número de placa no puede exceder los 7 caracteres.");

            RuleFor(x => x.RateTypeId)
                .GreaterThan(0).WithMessage("La tarifa debe ser mayor a 0.");
        }
    }
}