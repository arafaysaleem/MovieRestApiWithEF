using Newtonsoft.Json;
using System.Net;

namespace Entities.Responses
{
    public class ApiResponse
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public int StatusCode { get; private set; }
        public string? Details { get; private set; }

        public object Body { get; private set; }

        public ApiResponse(
            string message,
            HttpStatusCode statusCode,
            object body,
            string? details = null,
            bool success = true)
        {
            this.Success = success;
            this.Message = message;
            this.StatusCode = (int)statusCode;
            this.Details = details;
            this.Body = body;
        }

        public override string ToString()
        {
            // Format output into
            // {
            //   Headers: {
            //      Success: bool,
            //      Message: string,
            //      StatusCode: int,
            //      Details: string?
            //   },
            //   Body: object
            // }
            var res = new
            {
                Headers = new
                {
                    this.Success,
                    this.Message,
                    this.StatusCode,
                    this.Details
                },
                this.Body
            };

            // Convert model to Json
            return JsonConvert.SerializeObject(res);
        }
    }
}
