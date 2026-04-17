using FluentValidation;

namespace ParkingManagementSystem.Application.Features.ParkingEntries.Commands.RegisterVehicleExit
{
    public class RegisterVehicleExitCommandValidator : AbstractValidator<RegisterVehicleExitCommand>
    {
        public RegisterVehicleExitCommandValidator()
        {
            RuleFor(x => x.TicketNumber)
                .NotEmpty().WithMessage("El número de ticket es requerido.")
                .MaximumLength(30).WithMessage("El número de ticket no puede exceder los 30 caracteres.");
        }
    }
}