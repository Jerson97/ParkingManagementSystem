using MediatR;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Application.Features.Subscriptions.Queries.GetSubscriptions
{
    public class GetSubscriptionsQuery : IRequest<MessageResult<List<GetSubscriptionsResponseDto>>>
    {
 
    }

    public class GetSubscriptionsQueryHandler : IRequestHandler<GetSubscriptionsQuery, MessageResult<List<GetSubscriptionsResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSubscriptionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<List<GetSubscriptionsResponseDto>>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var subscriptions = await _unitOfWork.Subscriptions.GetAllAsync(cancellationToken);

            var response = subscriptions.Select(s => new GetSubscriptionsResponseDto
            {
                Id = s.Id,
                CustomerName = s.CustomerName,
                PhoneNumber = s.PhoneNumber,
                LicensePlate = s.Vehicle.LicensePlate,
                RateTypeName = s.RateType.Name,
                SpaceNumber = s.ParkingSpace.SpaceNumber,
                Status = s.Status == SubscriptionStatus.Active ? "Activo" : "Vencido",
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                //IsExpired = s.EndDate < DateTime.Now
            }).ToList();

            return MessageResult<List<GetSubscriptionsResponseDto>>.Of("Consulta realizada correctamente.", response);
        }
    }
}