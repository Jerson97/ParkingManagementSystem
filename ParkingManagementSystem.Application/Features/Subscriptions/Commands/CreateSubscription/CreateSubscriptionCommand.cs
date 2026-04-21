using MediatR;
using ParkingManagementSystem.Application.Common.Extensions;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Entities;
using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Application.Features.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommand : IRequest<MessageResult<int>>
    {
        public string CustomerName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;
        public int RateTypeId { get; set; }
        public int ParkingSpaceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, MessageResult<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateSubscriptionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<MessageResult<int>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var rateType = await _unitOfWork.RateTypes
                .GetByIdAsync(request.RateTypeId, cancellationToken);

            if (rateType is null)
                ServiceStatus.NotFound.Throw("La tarifa no existe.");

            var parkingSpace = await _unitOfWork.ParkingSpaces
                .GetByIdAsync(request.ParkingSpaceId, cancellationToken);

            if (parkingSpace is null)
                ServiceStatus.NotFound.Throw("El espacio no existe.");

            var spaceAlreadyAssigned = await _unitOfWork.Subscriptions
                .ExistsByParkingSpaceIdAsync(request.ParkingSpaceId, cancellationToken);

            if (spaceAlreadyAssigned)
                ServiceStatus.BadRequest.Throw("El espacio ya está asignado a un abonado.");

            var vehicle = await _unitOfWork.Vehicles
                .GetByLicensePlateAsync(request.LicensePlate, cancellationToken);

            if (vehicle is null)
            {
                vehicle = new Vehicle
                {
                    LicensePlate = request.LicensePlate
                };

                await _unitOfWork.Vehicles.AddAsync(vehicle, cancellationToken);
            }

            var vehicleAlreadyAssigned = await _unitOfWork.Subscriptions
                .ExistsByLicensePlateAsync(request.LicensePlate, cancellationToken);

            if (vehicleAlreadyAssigned)
                ServiceStatus.BadRequest.Throw("El vehículo ya tiene una suscripción registrada.");

            var subscription = new Subscription
            {
                CustomerName = request.CustomerName,
                PhoneNumber = request.PhoneNumber,
                Status = SubscriptionStatus.Active,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Vehicle = vehicle,
                RateTypeId = request.RateTypeId,
                ParkingSpaceId = request.ParkingSpaceId
            };

            await _unitOfWork.Subscriptions.AddAsync(subscription, cancellationToken);

            parkingSpace.Status = ParkingSpaceStatus.Reserved;
            _unitOfWork.ParkingSpaces.Update(parkingSpace);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MessageResult<int>.Of("Abonado registrado correctamente.", subscription.Id);
        }
    }
}