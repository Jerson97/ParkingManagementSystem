using FluentValidation;

namespace ParkingManagementSystem.Application.Features.RateType.Commands.CreateRateType
{
    public class CreateRateTypeCommandValidator : AbstractValidator<CreateRateTypeCommand>
    {
        public CreateRateTypeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre de la tarifa es requerido.")
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");
        }
    }
}
