using Entities.Responses;
using MovieRestApiWithEF.API.Exceptions;
using MovieRestApiWithEF.Infrastructure;
using System.Net;

namespace MovieRestApiWithEF.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Wrap in try catch to capture any unhandled exception from api or server
            try
            {
                // Forward the request to normal pipelines
                await _next(context);

                if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    throw new ForbiddenException("User does not have permission for the action.");
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedException("Authentication failed.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Caught: {ex}");
                await HandleExceptionAsync(context, ex); // To transform exception to error response
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                BaseException e => e.StatusCode, // If api exception, get its status code
                _ => HttpStatusCode.InternalServerError // Else 500 for other unfamiliar exceptions
            };

            context.Response.StatusCode = (int)statusCode;

            var data = exception switch
            {
                UnprocessibleEntityException e => e.Details, // If 422, then get list of validation errors
                _ => null // Else no validation errors
            };

            // Generate a consistent api response from this info
            var errorResponse = new ApiResponse(
                success: false,
                message: exception.Message,
                code: exception.GetType().Name,
                statusCode: statusCode,
                body: new { },
                details: data
            );

            // Return the api response from the middleware to the client
            return context.Response.WriteAsync(errorResponse.ToString());
        }
    }
}
