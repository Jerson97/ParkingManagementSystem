using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Features.Dashboard.Queries;

namespace ParkingManagementSystem.WebApi.Controllers
{
    [Authorize]
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : MyBaseController
    {
        [HttpGet("summary")]
        public async Task<ActionResult<MessageResult<GetDashboardSummaryResponseDto>>> GetSummary()
        {
            var result = await Mediator.Send(new GetDashboardSummaryQuery());
            return Ok(result);
        }
    }
}
