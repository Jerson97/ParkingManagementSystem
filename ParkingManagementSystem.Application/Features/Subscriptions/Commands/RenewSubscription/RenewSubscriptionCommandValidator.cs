using FluentValidation;

namespace ParkingManagementSystem.Application.Features.Subscriptions.Commands.RenewSubscription
{
    public class RenewSubscriptionCommandValidator : AbstractValidator<RenewSubscriptionCommand>
    {
        public RenewSubscriptionCommandValidator()
        {
            RuleFor(x => x.SubscriptionId)
                .GreaterThan(0).WithMessage("La suscripción debe ser mayor a 0.");
        }
    }
}
