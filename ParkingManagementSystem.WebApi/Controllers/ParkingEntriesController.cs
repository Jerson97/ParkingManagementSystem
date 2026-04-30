using Microsoft.AspNetCore.Mvc;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Features.ParkingEntries.Commands.CreateParkingEntry;
using ParkingManagementSystem.Application.Features.ParkingEntries.Commands.RegisterVehicleExit;
using ParkingManagementSystem.Application.Features.ParkingEntries.Queries.GetParkingEntryByTicket;
using ParkingManagementSystem.Application.Features.ParkingEntries.Queries.GetParkingEntryHistory;
using ParkingManagementSystem.Application.Features.ParkingEntries.Queries.GetParkingFee;

namespace ParkingManagementSystem.WebApi.Controllers
{
    public class ParkingEntriesController : MyBaseController
    {
        [HttpPost]
        public async Task<ActionResult<MessageResult<int>>> Create([FromBody] CreateParkingEntryCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("exit")]
        public async Task<ActionResult<MessageResult<int>>> RegisterExit([FromBody] RegisterVehicleExitCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("calculate")]
        public async Task<ActionResult<MessageResult<GetParkingFeeResponseDto>>> CalculateFee([FromBody] GetParkingFeeQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<ActionResult<MessageResult<List<GetParkingEntryHistoryResponseDto>>>> GetHistory([FromQuery] GetParkingEntryHistoryQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{ticketNumber}")]
        public async Task<ActionResult<MessageResult<GetParkingEntryByTicketResponseDto>>> GetByTicket(string ticketNumber)
        {
            var result = await Mediator.Send(new GetParkingEntryByTicketQuery
            {
                TicketNumber = ticketNumber
            });

            return Ok(result);
        }

    }
}
