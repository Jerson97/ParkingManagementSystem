using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ParkingManagementSystem.Application.Common.Extensions;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Application.Features.Subscriptions.Commands.CancelSubscription
{
    public class CancelSubscriptionCommand : IRequest<MessageResult<int>>
    {
        public int Id { get; set; }
    }

    public class CancelSubscriptionCommandHandler : IRequestHandler<CancelSubscriptionCommand, MessageResult<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelSubscriptionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<int>> Handle(CancelSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _unitOfWork.Subscriptions
                .GetByIdAsync(request.Id, cancellationToken);

            if (subscription is null)
                ServiceStatus.NotFound.Throw("No se encontró el abonado.");

            if (subscription.Status != SubscriptionStatus.Active)
                ServiceStatus.BadRequest.Throw("El abonado no está activo.");

            subscription.Status = SubscriptionStatus.Expired;

            subscription.ParkingSpace.Status = ParkingSpaceStatus.Available;

            _unitOfWork.Subscriptions.Update(subscription);
            _unitOfWork.ParkingSpaces.Update(subscription.ParkingSpace);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MessageResult<int>.Of("Abonado cancelado correctamente.", subscription.Id);
        }
    }
}
