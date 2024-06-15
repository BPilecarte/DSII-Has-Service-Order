namespace OsDsII.Api.Dtos.ServiceOrders
{
    public record CreateServiceOrderDto(
        string Description,
        double Price,
        int CustomerId
    );
}
