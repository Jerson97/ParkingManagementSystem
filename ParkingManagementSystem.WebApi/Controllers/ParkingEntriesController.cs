using Microsoft.AspNetCore.Mvc;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.Features.ParkingEntries.Commands.CreateParkingEntry;

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
    }
}
