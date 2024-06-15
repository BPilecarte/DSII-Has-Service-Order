using OsDsII.Api.Dtos.ServiceOrders;

namespace OsDsII.Api.Services.ServiceOrders
{
    public interface IServiceOrderService
    {
        public Task<List<ServiceOrderDto>> GetAllAsync();
        public Task<ServiceOrderDto> GetServiceOrderAsync(int id);
        public Task<ServiceOrderDto> GetServiceOrderFromUserAsync(int serviceOrderId);
        public Task<ServiceOrderDto> GetServiceOrderWithComments(int serviceOrderId);
        public Task<NewServiceOrderDto> CreateServiceOrderAsync(CreateServiceOrderDto createServiceOrderDto);
        public Task FinishServiceOrderAsync(int id);
        public Task CancelServiceOrderAsync(int id);
    }
}
