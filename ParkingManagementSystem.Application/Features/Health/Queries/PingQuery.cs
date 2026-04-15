using ParkingManagementSystem.Application.Common.Results;
using MediatR;

namespace ParkingManagementSystem.Application.Features.Health.Queries
{
    public class PingQuery : IRequest<MessageResult<string>>
    {
    }

    public class PingQueryHandler : IRequestHandler<PingQuery, MessageResult<string>>
    {
        public Task<MessageResult<string>> Handle(PingQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(MessageResult<string>.Of("OK", "Pong"));
        }
    }
}