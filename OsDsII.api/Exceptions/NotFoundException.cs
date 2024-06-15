using System.Net;

namespace OsDsII.Api.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) :
        base
            (
                "HSO-002",
                message,
                HttpStatusCode.NoContent,
                StatusCodes.Status404NotFound,
                null,
                DateTimeOffset.UtcNow,
                null
            )
        { }

    }
}
