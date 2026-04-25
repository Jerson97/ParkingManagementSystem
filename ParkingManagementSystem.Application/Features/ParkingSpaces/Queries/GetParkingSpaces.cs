using MediatR;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Application.Features.ParkingSpaces.Queries
{
    public class GetParkingSpacesQuery : IRequest<MessageResult<List<GetParkingSpacesResponseDto>>>
    {
    }

    public class GetParkingSpacesQueryHandler : IRequestHandler<GetParkingSpacesQuery, MessageResult<List<GetParkingSpacesResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetParkingSpacesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<List<GetParkingSpacesResponseDto>>> Handle(GetParkingSpacesQuery request, CancellationToken cancellationToken)
        {
            var parkingSpaces = await _unitOfWork.ParkingSpaces
                .GetAllAsync(cancellationToken);

            var response = parkingSpaces.Select(ps =>
            {
                var activeEntry = ps.ParkingEntries
                    .FirstOrDefault(pe => pe.Status == ParkingEntryStatus.Inside);

                return new GetParkingSpacesResponseDto
                {
                    Id = ps.Id,
                    SpaceNumber = ps.SpaceNumber,
                    Status = ps.Status switch
                    {
                        ParkingSpaceStatus.Available => "Disponible",
                        ParkingSpaceStatus.Occupied => "Ocupado",
                        ParkingSpaceStatus.Reserved => "Reservado",
                        _ => ps.Status.ToString()
                    },
                    LicensePlate = ps.Status == ParkingSpaceStatus.Occupied
                        ? activeEntry?.Vehicle.LicensePlate
                        : ps.Status == ParkingSpaceStatus.Reserved
                            ? ps.Subscription?.Vehicle.LicensePlate
                            : null,
                    CustomerName = ps.Status == ParkingSpaceStatus.Reserved
                        ? ps.Subscription?.CustomerName
                        : null
                };
            }).ToList();

            return MessageResult<List<GetParkingSpacesResponseDto>>.Of("Consulta realizada correctamente.", response);
        }
    }
}
