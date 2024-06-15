namespace OsDsII.Api.Dtos.Comments
{
    public record CommentDto(long Id, string Description, DateTimeOffset SendDate, int ServiceOrderId)
    {

    }
}