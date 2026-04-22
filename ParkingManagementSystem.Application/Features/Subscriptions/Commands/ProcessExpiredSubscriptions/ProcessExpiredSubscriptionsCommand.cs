using MediatR;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Application.Features.Subscriptions.Commands.ProcessExpiredSubscriptions
{
    public class ProcessExpiredSubscriptionsCommand : IRequest<MessageResult<List<ProcessExpiredSubscriptionsResponseDto>>>
    {
    }

    public class ProcessExpiredSubscriptionsCommandHandler : IRequestHandler<ProcessExpiredSubscriptionsCommand, MessageResult<List<ProcessExpiredSubscriptionsResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProcessExpiredSubscriptionsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<List<ProcessExpiredSubscriptionsResponseDto>>> Handle(ProcessExpiredSubscriptionsCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;

            var expiredSubscriptions = await _unitOfWork.Subscriptions
                .GetExpiredSubscriptionsAsync(now, cancellationToken);

            if (!expiredSubscriptions.Any())
            {
                return MessageResult<List<ProcessExpiredSubscriptionsResponseDto>>
                    .Of("No se encontraron suscripciones vencidas.", new List<ProcessExpiredSubscriptionsResponseDto>());
            }

            var response = new List<ProcessExpiredSubscriptionsResponseDto>();

            foreach (var sub in expiredSubscriptions)
            {
                sub.Status = SubscriptionStatus.Expired;

                sub.ParkingSpace.Status = ParkingSpaceStatus.Available;
                _unitOfWork.ParkingSpaces.Update(sub.ParkingSpace);

                response.Add(new ProcessExpiredSubscriptionsResponseDto
                {
                    SubscriptionId = sub.Id,
                    CustomerName = sub.CustomerName,
                    LicensePlate = sub.Vehicle.LicensePlate,
                    SpaceNumber = sub.ParkingSpace.SpaceNumber,
                    EndDate = sub.EndDate,
                    NewStatus = sub.Status.ToString()
                });
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MessageResult<List<ProcessExpiredSubscriptionsResponseDto>>
                .Of("Suscripciones vencidas procesadas correctamente.", response);
        }
    }
}