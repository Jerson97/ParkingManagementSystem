using Microsoft.AspNetCore.Mvc;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.DTOs;
using ParkingManagementSystem.Application.Features.Subscriptions.Commands.CreateSubscription;
using ParkingManagementSystem.Application.Features.Subscriptions.Commands.ProcessExpiredSubscriptions;
using ParkingManagementSystem.Application.Features.Subscriptions.Queries.GetSubscriptions;

namespace ParkingManagementSystem.WebApi.Controllers
{
    [Route("api/subscriptions")]
    [ApiController]
    public class SubscriptionsController : MyBaseController
    {
        [HttpPost]
        public async Task<ActionResult<MessageResult<int>>> Create([FromBody] CreateSubscriptionCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<MessageResult<List<GetSubscriptionsResponseDto>>>> GetAll()
        {
            var result = await Mediator.Send(new GetSubscriptionsQuery());
            return Ok(result);
        }

        [HttpPost("process-expired")]
        public async Task<ActionResult<MessageResult<List<ProcessExpiredSubscriptionsResponseDto>>>> ProcessExpired()
        {
            var result = await Mediator.Send(new ProcessExpiredSubscriptionsCommand());
            return Ok(result);
        }
    }
}
