using ParkingManagementSystem.WebApi.Middleware.ErrorMiddlewares;

namespace ParkingManagementSystem.WebApi.Midleware
{
    public static class InstallHandlerMiddleware
    {
        public static IApplicationBuilder UseHandlerUsers(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
