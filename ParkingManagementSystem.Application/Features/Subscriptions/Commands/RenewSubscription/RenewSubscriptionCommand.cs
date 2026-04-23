using MediatR;
using ParkingManagementSystem.Application.Common.Extensions;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Application.Features.Subscriptions.Commands.RenewSubscription
{
    public class RenewSubscriptionCommand : IRequest<MessageResult<int>>
    {
        public int SubscriptionId { get; set; }
        //public int DaysToAdd { get; set; }
    }

    public class RenewSubscriptionCommandHandler : IRequestHandler<RenewSubscriptionCommand, MessageResult<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RenewSubscriptionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<int>> Handle(RenewSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _unitOfWork.Subscriptions
                .GetByIdAsync(request.SubscriptionId, cancellationToken);

            if (subscription is null)
                ServiceStatus.NotFound.Throw("No se encontró la suscripción.");

            int daysToAdd = subscription.RateType.Name switch
            {
                "Semanal Noche" => 7,
                "Mensual Noche" => 30,
                "Mensual Día + Noche" => 30,
                _ => 0
            };

            if (daysToAdd <= 0)
                ServiceStatus.BadRequest.Throw("La tarifa de la suscripción no tiene una duración configurada para renovación.");

            var now = DateTime.Now;

            if (subscription.EndDate >= now)
            {
                subscription.EndDate = subscription.EndDate.AddDays(daysToAdd);
            }
            else
            {
                subscription.StartDate = now;
                subscription.EndDate = now.AddDays(daysToAdd);
            }

            subscription.Status = SubscriptionStatus.Active;
            subscription.ParkingSpace.Status = ParkingSpaceStatus.Reserved;

            _unitOfWork.Subscriptions.Update(subscription);
            _unitOfWork.ParkingSpaces.Update(subscription.ParkingSpace);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MessageResult<int>.Of("Suscripción renovada correctamente.", subscription.Id);
        }
    }
}
