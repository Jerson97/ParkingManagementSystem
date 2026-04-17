using MediatR;
using ParkingManagementSystem.Application.Common.Extensions;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Application.Features.ParkingEntries.Queries.GetParkingFee
{
    public class GetParkingFeeQuery : IRequest<MessageResult<GetParkingFeeResponseDto>>
    {
        public string TicketNumber { get; set; } = null!;
    }

    public class GetParkingFeeQueryHandler : IRequestHandler<GetParkingFeeQuery, MessageResult<GetParkingFeeResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetParkingFeeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<GetParkingFeeResponseDto>> Handle(GetParkingFeeQuery request, CancellationToken cancellationToken)
        {
            var parkingEntry = await _unitOfWork.ParkingEntries
                .GetByTicketNumberAsync(request.TicketNumber, cancellationToken);

            if (parkingEntry is null)
                ServiceStatus.NotFound.Throw("No se encontró el ticket.");

            if (parkingEntry.Status != ParkingEntryStatus.Inside)
                ServiceStatus.BadRequest.Throw("El ticket ya fue cerrado.");

            var currentTime = DateTime.Now;
            decimal totalAmount;

            if (parkingEntry.RateType.IsHourly)
            {
                var duration = currentTime - parkingEntry.EntryTime;
                var fullHours = Math.Floor(duration.TotalHours);
                var remainingMinutes = duration.Minutes;

                var hoursToBill = remainingMinutes > 10
                    ? Math.Ceiling(duration.TotalHours)
                    : Math.Max(1, fullHours);

                totalAmount = (decimal)hoursToBill * parkingEntry.RateType.Price;
            }
            else
            {
                totalAmount = parkingEntry.RateType.Price;
            }

            var response = new GetParkingFeeResponseDto
            {
                TicketNumber = parkingEntry.TicketNumber,
                LicensePlate = parkingEntry.Vehicle.LicensePlate,
                RateTypeName = parkingEntry.RateType.Name,
                EntryTime = parkingEntry.EntryTime,
                CurrentTime = currentTime,
                EstimatedAmount = totalAmount,
                SpaceNumber = parkingEntry.ParkingSpace.SpaceNumber
            };

            return MessageResult<GetParkingFeeResponseDto>.Of("Consulta realizada correctamente.", response);
        }
    }

}
