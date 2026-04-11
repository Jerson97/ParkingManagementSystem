namespace CleanTemplate.Application.Common.Results
{
    public enum ServiceStatus
    {
        Success,
        Created = 201,
        FailedValidation = 400,
        Forbidden = 403,
        InternalError = 500,
        Unauthorized = 401,
        UnprocessableEntity = 422,
        NotFound = 404,
        NoContent = 204,
        BadRequest = 400,
        Error = 520
    }
}