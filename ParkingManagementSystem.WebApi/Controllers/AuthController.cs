using Microsoft.AspNetCore.Mvc;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Features.Auth.Commands.Login;

namespace ParkingManagementSystem.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : MyBaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<MessageResult<LoginResponseDto>>> Login([FromBody] LoginCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
