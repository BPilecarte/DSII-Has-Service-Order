using OsDsII.Api.Models;

namespace OsDsII.Api.Dtos.ServiceOrders
{
    public record ServiceOrderDto(
        int Id,
        string Description,
        double Price,
        StatusServiceOrder Status,
        DateTimeOffset OpeningDate,
        DateTimeOffset FinishDate,
        List<Comment> Comments //CommentDTO
        );
}
