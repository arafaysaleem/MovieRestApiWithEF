using Contracts;
using Entities.Responses;
using MovieRestApiWithEF.Exceptions;
using System.Net;

namespace MovieRestApiWithEF.Middlewares
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
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Caught: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                BaseException e => e.StatusCode,
                _ => HttpStatusCode.InternalServerError
            };

            var data = exception switch
            {
                UnprocessibleEntityException e => e.Details,
                _ => null
            };

            var errorResponse = new ApiResponse(
                success: false,
                message: exception.Message,
                statusCode: statusCode,
                body: new { },
                details: data
            );

            return context.Response.WriteAsync(errorResponse.ToString());
        }
    }
}
