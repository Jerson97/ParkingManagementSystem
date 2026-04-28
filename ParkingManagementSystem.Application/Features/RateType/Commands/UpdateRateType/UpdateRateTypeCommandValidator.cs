using FluentValidation;

namespace ParkingManagementSystem.Application.Features.RateType.Commands.UpdateRateType
{
    public class UpdateRateTypeCommandValidator : AbstractValidator<UpdateRateTypeCommand>
    {
        public UpdateRateTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("La tarifa debe ser mayor a 0.");

            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.")
                .When(x => x.Price.HasValue);
        }
    }
}
