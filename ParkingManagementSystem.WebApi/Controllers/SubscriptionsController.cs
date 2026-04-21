using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingManagementSystem.Application.Common.Results;
using ParkingManagementSystem.Application.Features.Subscriptions.Commands.CreateSubscription;

namespace ParkingManagementSystem.WebApi.Controllers
{
    public class SubscriptionsController : MyBaseController
    {
        [HttpPost]
        public async Task<ActionResult<MessageResult<int>>> Create([FromBody] CreateSubscriptionCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}