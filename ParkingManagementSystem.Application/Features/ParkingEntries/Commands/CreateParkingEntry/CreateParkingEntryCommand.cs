using MediatR;
using ParkingManagementSystem.Application.Common.Extensions;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Entities;
using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Application.Features.ParkingEntries.Commands.CreateParkingEntry
{
    public class CreateParkingEntryCommand : IRequest<MessageResult<CreateParkingEntryResponseDto>>
    {
        public string LicensePlate { get; set; } = null!;
        public int RateTypeId { get; set; }
    }

    public class CreateParkingEntryCommandHandler : IRequestHandler<CreateParkingEntryCommand, MessageResult<CreateParkingEntryResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateParkingEntryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<CreateParkingEntryResponseDto>> Handle(CreateParkingEntryCommand request, CancellationToken cancellationToken)
        {
            var licensePlate = request.LicensePlate.Trim().ToUpper();

            var rateType = await _unitOfWork.RateTypes
                .GetByIdAsync(request.RateTypeId, cancellationToken);

            if (rateType is null)
                ServiceStatus.NotFound.Throw("La tarifa no existe.");

            var hasActiveEntry = await _unitOfWork.ParkingEntries
                .ExistsActiveEntryByLicensePlateAsync(licensePlate, cancellationToken);

            if (hasActiveEntry)
                ServiceStatus.BadRequest.Throw("El vehículo ya tiene un ingreso activo.");

            var vehicle = await _unitOfWork.Vehicles
                .GetByLicensePlateAsync(licensePlate, cancellationToken);

            if (vehicle is null)
            {
                vehicle = new Vehicle
                {
                    LicensePlate = licensePlate
                };

                await _unitOfWork.Vehicles.AddAsync(vehicle, cancellationToken);
            }

            var parkingSpace = await _unitOfWork.ParkingSpaces
                .GetAvailableSpaceAsync(cancellationToken);

            if (parkingSpace is null)
                ServiceStatus.BadRequest.Throw("No hay espacios disponibles.");

            var ticket = $"T-{DateTime.Now:yyyyMMddHHmmss}";

            var parkingEntry = new ParkingEntry
            {
                TicketNumber = ticket,
                EntryTime = DateTime.Now,
                Status = ParkingEntryStatus.Inside,
                PaymentStatus = PaymentStatus.Pending,
                Vehicle = vehicle,
                RateTypeId = rateType.Id,
                ParkingSpaceId = parkingSpace.Id
            };

            await _unitOfWork.ParkingEntries.AddAsync(parkingEntry, cancellationToken);

            parkingSpace.Status = ParkingSpaceStatus.Occupied;
            _unitOfWork.ParkingSpaces.Update(parkingSpace);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new CreateParkingEntryResponseDto
            {
                ParkingEntryId = parkingEntry.Id,
                TicketNumber = parkingEntry.TicketNumber,
                LicensePlate = licensePlate,
                RateTypeName = rateType.Name,
                EntryTime = parkingEntry.EntryTime,
                SpaceNumber = parkingSpace.SpaceNumber,
                RateTypePrice = rateType.Price,
                IsHourly = rateType.IsHourly,
                PaymentStatus = parkingEntry.PaymentStatus.ToString(),
                Status = parkingEntry.Status.ToString()
            };

            return MessageResult<CreateParkingEntryResponseDto>.Of("Ingreso registrado correctamente.",response);
        }
    }
}