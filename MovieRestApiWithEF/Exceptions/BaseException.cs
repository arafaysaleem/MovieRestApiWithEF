using System.Net;

namespace MovieRestApiWithEF.API.Exceptions
{
    public class BaseException : Exception
    {
        private readonly HttpStatusCode statusCode;

        public BaseException(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
        }

        public BaseException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            this.statusCode = statusCode;
        }

        public BaseException(HttpStatusCode statusCode, string message, Exception ex)
            : base(message, ex)
        {
            this.statusCode = statusCode;
        }


        public HttpStatusCode StatusCode
        {
            get { return statusCode; }
        }
    }
}
