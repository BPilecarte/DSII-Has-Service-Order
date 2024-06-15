using OsDsII.Api.Dtos.ServiceOrders;

namespace OsDsII.Api.Dtos.Customers
{
    public record CustomerDto(
        string Name,
        string Email,
        string Phone,
        List<ServiceOrderDto> ListServiceOrder);
}
