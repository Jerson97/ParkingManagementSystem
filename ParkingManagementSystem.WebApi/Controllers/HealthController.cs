using ParkingManagementSystem.Application.Features.Health.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ParkingManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : MyBaseController
    {

        [HttpGet("ping")]
        public async Task<IActionResult> Ping()
        {
            var result = await Mediator.Send(new PingQuery());

            return Ok(result);
        }
    }
}