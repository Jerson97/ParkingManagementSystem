using MediatR;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Application.Features.Dashboard.Queries
{
    public class GetDashboardSummaryQuery : IRequest<MessageResult<GetDashboardSummaryResponseDto>>
    {
    }

    public class GetDashboardSummaryQueryHandler
        : IRequestHandler<GetDashboardSummaryQuery, MessageResult<GetDashboardSummaryResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDashboardSummaryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<GetDashboardSummaryResponseDto>> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
        {
            var today = DateTime.Now;

            var response = new GetDashboardSummaryResponseDto
            {
                TotalSpaces = await _unitOfWork.ParkingSpaces.CountAllAsync(cancellationToken),

                AvailableSpaces = await _unitOfWork.ParkingSpaces
                    .CountByStatusAsync(ParkingSpaceStatus.Available, cancellationToken),

                OccupiedSpaces = await _unitOfWork.ParkingSpaces
                    .CountByStatusAsync(ParkingSpaceStatus.Occupied, cancellationToken),

                ReservedSpaces = await _unitOfWork.ParkingSpaces
                    .CountByStatusAsync(ParkingSpaceStatus.Reserved, cancellationToken),

                ActiveSubscriptions = await _unitOfWork.Subscriptions
                    .CountByStatusAsync(SubscriptionStatus.Active, cancellationToken),

                ExpiredSubscriptions = await _unitOfWork.Subscriptions
                    .CountByStatusAsync(SubscriptionStatus.Expired, cancellationToken),

                TodayRevenue = await _unitOfWork.ParkingEntries
                    .GetTodayRevenueAsync(today, cancellationToken),

                ClosedTicketsToday = await _unitOfWork.ParkingEntries
                    .CountClosedTodayAsync(today, cancellationToken)
            };

            return MessageResult<GetDashboardSummaryResponseDto>.Of("Resumen obtenido correctamente.", response);
        }
    }
}
