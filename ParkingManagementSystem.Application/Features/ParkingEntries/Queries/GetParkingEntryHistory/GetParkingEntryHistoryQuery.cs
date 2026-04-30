using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Interfaces.Repositories;
using ParkingManagementSystem.Domain.Enums;

namespace ParkingManagementSystem.Application.Features.ParkingEntries.Queries.GetParkingEntryHistory
{
    public class GetParkingEntryHistoryQuery: IRequest<MessageResult<List<GetParkingEntryHistoryResponseDto>>>
    {
        public string? LicensePlate { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }

    public class GetParkingEntryHistoryQueryHandler: IRequestHandler<GetParkingEntryHistoryQuery, MessageResult<List<GetParkingEntryHistoryResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetParkingEntryHistoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResult<List<GetParkingEntryHistoryResponseDto>>> Handle(
            GetParkingEntryHistoryQuery request,
            CancellationToken cancellationToken)
        {
            var history = await _unitOfWork.ParkingEntries
                .GetHistoryAsync(request.LicensePlate, request.From, request.To, cancellationToken);

            var response = history.Select(pe => new GetParkingEntryHistoryResponseDto
            {
                Id = pe.Id,
                TicketNumber = pe.TicketNumber,
                LicensePlate = pe.Vehicle.LicensePlate,
                SpaceNumber = pe.ParkingSpace.SpaceNumber,
                RateTypeName = pe.RateType.Name,
                EntryTime = pe.EntryTime,
                ExitTime = pe.ExitTime,
                TotalAmount = pe.TotalAmount ?? 0,
                PaymentStatus = pe.PaymentStatus == PaymentStatus.Paid ? "Pagado" : "Pendiente",
                Status = pe.Status == ParkingEntryStatus.Completed ? "Completado" : pe.Status.ToString()
            }).ToList();

            return MessageResult<List<GetParkingEntryHistoryResponseDto>>.Of("Historial consultado correctamente.", response);
        }
    }
}
