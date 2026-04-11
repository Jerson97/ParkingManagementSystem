using CleanTemplate.WebApi.Middleware.ErrorMiddlewares;

namespace CleanTemplate.WebApi.Midleware
{
    public static class InstallHandlerMiddleware
    {
        public static IApplicationBuilder UseHandlerUsers(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
