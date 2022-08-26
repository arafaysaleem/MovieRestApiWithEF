using System.Net;

namespace MovieRestApiWithEF.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message) { }
    }

    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message) { }
    }

    public class UnprocessibleEntityException : BaseException
    {
        public string Details { get; private set; }
        public UnprocessibleEntityException(string message, string details) : base(
            HttpStatusCode.UnprocessableEntity, 
            message)
        {
            this.Details = details;
        }
    }

    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message) : base(HttpStatusCode.Unauthorized, message) { }
    }

    public class ForbiddenException : BaseException
    {
        public ForbiddenException(string message) : base(HttpStatusCode.Forbidden, message) { }
    }

    public class InternalServerException : BaseException
    {
        public InternalServerException(string message) : base(HttpStatusCode.InternalServerError, message) { }
    }
}
