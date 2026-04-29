using Microsoft.AspNetCore.Mvc;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Features.RateType.Commands.CreateRateType;
using ParkingManagementSystem.Application.Features.RateType.Commands.DeleteRateType;
using ParkingManagementSystem.Application.Features.RateType.Commands.UpdateRateType;
using ParkingManagementSystem.Application.Features.RateType.Queries.GetRateType;

namespace ParkingManagementSystem.WebApi.Controllers
{
    [Route("api/rate-types")]
    [ApiController]
    public class RateTypesController : MyBaseController
    {
        [HttpGet]
        public async Task<ActionResult<MessageResult<List<GetRateTypesResponseDto>>>> GetAll()
        {
            var result = await Mediator.Send(new GetRateTypesQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MessageResult<int>>> Create([FromBody] CreateRateTypeCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<MessageResult<int>>> Update(int id, [FromBody] UpdateRateTypeCommand command)
        {
            command.Id = id;

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResult<int>>> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteRateTypeCommand { Id = id });
            return Ok(result);
        }
    }
}
