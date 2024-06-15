using System.Net;

namespace OsDsII.Api.Exceptions
{
    public class BadRequest : BaseException
    {
        public BadRequest(string message) :
        base
            (
                "HSO-003", 
                message,
                HttpStatusCode.BadRequest,
                StatusCodes.Status400BadRequest,
                null,
                DateTimeOffset.UtcNow,
                null
            )
        { }

    }
}
