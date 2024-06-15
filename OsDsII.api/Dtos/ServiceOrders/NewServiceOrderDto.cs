using OsDsII.Api.Models;

namespace OsDsII.Api.Dtos.ServiceOrders
{
    public record NewServiceOrderDto(
        int Id,
        string Description,
        double Price,
        StatusServiceOrder Status,
        DateTimeOffset
        OpeningDate,
        DateTimeOffset? FinishDate,
        int CustomerId
        );
}