using Microsoft.AspNetCore.Mvc;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Features.ParkingSpaces.Queries;

namespace ParkingManagementSystem.WebApi.Controllers
{
    [Route("api/parkingspaces")]
    [ApiController]
    public class ParkingSpacesController : MyBaseController
    {
        [HttpGet]
        public async Task<ActionResult<MessageResult<List<GetParkingSpacesResponseDto>>>> GetAll()
        {
            var result = await Mediator.Send(new GetParkingSpacesQuery());
            return Ok(result);
        }
    }
}