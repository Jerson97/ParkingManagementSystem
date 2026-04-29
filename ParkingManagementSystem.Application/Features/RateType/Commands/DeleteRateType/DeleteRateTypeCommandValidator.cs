using FluentValidation;

namespace ParkingManagementSystem.Application.Features.RateType.Commands.DeleteRateType
{
    public class DeleteRateTypeCommandValidator : AbstractValidator<DeleteRateTypeCommand>
    {
        public DeleteRateTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("La tarifa debe ser mayor a 0.");
        }
    }
}
