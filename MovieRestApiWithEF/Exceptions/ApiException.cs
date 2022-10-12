using System.Net;

namespace MovieRestApiWithEF.API.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message) { }
    }

    public class DuplicateEntryException : BaseException
    {
        public DuplicateEntryException(string message) : base(HttpStatusCode.Conflict, message) { }
    }

    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message) { }
    }

    public class UnprocessibleEntityException : BaseException
    {
        public object Details { get; private set; }
        public UnprocessibleEntityException(string message, object details) : base(
            HttpStatusCode.UnprocessableEntity,
            message)
        {
            Details = details;
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
