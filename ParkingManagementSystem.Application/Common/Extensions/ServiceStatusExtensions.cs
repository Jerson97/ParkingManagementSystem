using System.Net;
using ParkingManagementSystem.Application.Common.Exceptions;
using ParkingManagementSystem.Application.Common.Results;

namespace ParkingManagementSystem.Application.Common.Extensions
{
    public static class ServiceStatusExtensions
    {
        public static void Throw(this ServiceStatus status, string message)
        {
            HttpStatusCode code = status switch
            {
                ServiceStatus.NotFound => HttpStatusCode.NotFound,
                ServiceStatus.BadRequest => HttpStatusCode.BadRequest,
                ServiceStatus.Forbidden => HttpStatusCode.Forbidden,
                ServiceStatus.UnprocessableEntity => HttpStatusCode.UnprocessableEntity,
                _ => HttpStatusCode.InternalServerError
            };

            throw new ApiException(code, message);
        }
    }
}

