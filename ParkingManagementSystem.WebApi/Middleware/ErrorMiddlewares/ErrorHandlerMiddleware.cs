using System.Net;
using System.Text.Json;
using ParkingManagementSystem.Application.Common.Exceptions;
using ParkingManagementSystem.Application.Common.Results;

namespace ParkingManagementSystem.WebApi.Middleware.ErrorMiddlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ErrorHandlerAsync(context, ex);
            }
        }

        private async Task ErrorHandlerAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            string message;

            if (ex is ApiException apiEx)
            {
                statusCode = (int)apiEx.Code;
                message = apiEx.Message;
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = string.IsNullOrWhiteSpace(ex.Message)
                    ? "Error desconocido"
                    : ex.Message;
            }

            context.Response.StatusCode = statusCode;

            int customResponse = 0;

            if (statusCode == 404)
                customResponse = 0;
            else if (statusCode == 422)
                customResponse = 2;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(
                    MessageResult<object>.Of(message, null!, customResponse)
                )
            );
        }

    }
}
