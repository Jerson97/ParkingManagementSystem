using MediatR;
using ParkingManagementSystem.Application.Common.Extensions;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Interfaces.Repositories;

namespace ParkingManagementSystem.Application.Features.ParkingEntries.Queries.GetParkingEntryByTicket
{
    public class GetParkingEntryByTicketQuery: IRequest<MessageResult<GetParkingEntryByTicketResponseDto>>
    {
        public string TicketNumber { get; set; } = null!;
    }

    public class GetParkingEntryByTicketQueryHandler: IRequestHandler<GetParkingEntryByTicketQuery, MessageResult<GetParkingEntryByTicketResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetParkingEntryByTicketQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<GetParkingEntryByTicketResponseDto>> Handle(
            GetParkingEntryByTicketQuery request,
            CancellationToken cancellationToken)
        {
            var entry = await _unitOfWork.ParkingEntries
                .GetByTicketNumberAsync(request.TicketNumber, cancellationToken);

            if (entry is null)
                ServiceStatus.NotFound.Throw("No se encontró el ticket.");

            var response = new GetParkingEntryByTicketResponseDto
            {
                Id = entry.Id,
                TicketNumber = entry.TicketNumber,
                LicensePlate = entry.Vehicle.LicensePlate,
                SpaceNumber = entry.ParkingSpace.SpaceNumber,
                RateTypeName = entry.RateType.Name,
                EntryTime = entry.EntryTime,
                ExitTime = entry.ExitTime,
                TotalAmount = entry.TotalAmount,
                Status = entry.Status.ToString(),
                PaymentStatus = entry.PaymentStatus.ToString()
            };

            return MessageResult<GetParkingEntryByTicketResponseDto>.Of("Consulta realizada correctamente.", response);
        }
    }
}
