using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Features.ParkingSpaces.Queries;

namespace ParkingManagementSystem.WebApi.Controllers
{
    [Authorize(Roles = "Admin,Attendant")]
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