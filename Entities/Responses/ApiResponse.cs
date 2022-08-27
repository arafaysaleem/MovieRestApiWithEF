using Newtonsoft.Json;
using System.Net;

namespace Entities.Responses
{
    public class ApiResponse
    {
        public class ResponseHeaders
        {
            public bool Success { get; init; }
            public string? Code { get; init; }
            public int StatusCode { get; init; }
            public string Message { get; init; }
            public object? Details { get; init; }

            public ResponseHeaders(bool success, string message, int statusCode, object? details, string? code)
            {
                Success = success;
                Code = code;
                StatusCode = statusCode;
                Message = message;
                Details = details;
            }
        }

        public ResponseHeaders Headers { get; init; }

        public object Body { get; private set; }

        public ApiResponse(
            string message,
            HttpStatusCode statusCode,
            object body,
            object? details = null,
            string? code = "OK",
            bool success = true)
        {
            Headers = new ResponseHeaders(
                success: success,
                code: code,
                statusCode: (int)statusCode,
                message: message,
                details: details
            );
            Body = body;
        }

        public override string ToString()
        {
            // Format output into
            // {
            //   Headers: {
            //      Success: bool,
            //      Code: string?
            //      StatusCode: int,
            //      Message: string,
            //      Details: string?
            //   },
            //   Body: object
            // }

            // Convert model to Json
            return JsonConvert.SerializeObject(this);
        }
    }
}
