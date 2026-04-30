using FluentValidation;

namespace ParkingManagementSystem.Application.Features.Subscriptions.Commands.CancelSubscription
{
    public class CancelSubscriptionCommandValidator : AbstractValidator<CancelSubscriptionCommand>
    {
        public CancelSubscriptionCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El id debe ser mayor a 0.");
        }
    }
}
