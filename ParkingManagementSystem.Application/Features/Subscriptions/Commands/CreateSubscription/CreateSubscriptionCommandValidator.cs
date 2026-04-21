using FluentValidation;

namespace ParkingManagementSystem.Application.Features.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
    {
        public CreateSubscriptionCommandValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("El nombre del abonado es requerido.")
                .MaximumLength(150).WithMessage("El nombre no puede exceder los 150 caracteres.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("El número de teléfono es requerido.")
                .MaximumLength(20).WithMessage("El número de teléfono no puede exceder los 20 caracteres.");

            RuleFor(x => x.LicensePlate)
                .NotEmpty().WithMessage("El número de placa es requerido.")
                .MaximumLength(7).WithMessage("El número de placa no puede exceder los 7 caracteres.");

            RuleFor(x => x.RateTypeId)
                .GreaterThan(0).WithMessage("La tarifa debe ser mayor a 0.");

            RuleFor(x => x.ParkingSpaceId)
                .GreaterThan(0).WithMessage("El espacio debe ser mayor a 0.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("La fecha de inicio es requerida.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("La fecha de fin es requerida.")
                .GreaterThan(x => x.StartDate).WithMessage("La fecha de fin debe ser mayor a la fecha de inicio.");
        }
    }
}