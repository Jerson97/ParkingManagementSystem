using MediatR;
using ParkingManagementSystem.Application.Common.Extensions;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Application.Features.ParkingEntries.Commands.RegisterVehicleExit
{
    public class RegisterVehicleExitCommand : IRequest<MessageResult<RegisterVehicleExitResponseDto>>
    {
        public string TicketNumber { get; set; } = null!;
    }

    public class RegisterVehicleExitCommandHandler : IRequestHandler<RegisterVehicleExitCommand, MessageResult<RegisterVehicleExitResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterVehicleExitCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<MessageResult<RegisterVehicleExitResponseDto>> Handle(RegisterVehicleExitCommand request, CancellationToken cancellationToken)
        {
            var parkingEntry = await _unitOfWork.ParkingEntries
                .GetByTicketNumberAsync(request.TicketNumber, cancellationToken);

            if (parkingEntry is null)
                ServiceStatus.NotFound.Throw("No se encontró el ticket.");

            if (parkingEntry.Status != ParkingEntryStatus.Inside)
                ServiceStatus.BadRequest.Throw("El ticket ya fue cerrado o no está activo.");

            var exitTime = DateTime.Now;
            decimal totalAmount;

            if (parkingEntry.RateType.IsHourly)
            {
                var duration = exitTime - parkingEntry.EntryTime;
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

            parkingEntry.ExitTime = exitTime;
            parkingEntry.TotalAmount = totalAmount;
            parkingEntry.Status = ParkingEntryStatus.Completed;
            parkingEntry.PaymentStatus = PaymentStatus.Paid;

            parkingEntry.ParkingSpace.Status = ParkingSpaceStatus.Available;

            _unitOfWork.ParkingEntries.Update(parkingEntry);
            _unitOfWork.ParkingSpaces.Update(parkingEntry.ParkingSpace);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new RegisterVehicleExitResponseDto
            {
                ParkingEntryId = parkingEntry.Id,
                TicketNumber = parkingEntry.TicketNumber,
                LicensePlate = parkingEntry.Vehicle.LicensePlate,
                RateTypeName = parkingEntry.RateType.Name,
                EntryTime = parkingEntry.EntryTime,
                ExitTime = parkingEntry.ExitTime!.Value,
                TotalAmount = parkingEntry.TotalAmount!.Value,
                PaymentStatus = parkingEntry.PaymentStatus.ToString(),
                Status = parkingEntry.Status.ToString(),
                SpaceNumber = parkingEntry.ParkingSpace.SpaceNumber
            };

            return MessageResult<RegisterVehicleExitResponseDto>.Of("Salida registrada correctamente.", response);
        }
    }
}
