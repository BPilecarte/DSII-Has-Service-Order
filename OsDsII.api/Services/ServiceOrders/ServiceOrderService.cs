using AutoMapper;
using OsDsII.Api.Dtos.ServiceOrders;
using OsDsII.Api.Models;
using OsDsII.Api.Repository.CustomersRepository;
using OsDsII.Api.Repository.ServiceOrderRepository;
using OsDsII.Api.Exceptions;

namespace OsDsII.Api.Services.ServiceOrders
{
    public sealed class ServiceOrderService : IServiceOrderService
    {
        private readonly IServiceOrderRepository _serviceOrderRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;

        public ServiceOrderService(IServiceOrderRepository serviceOrderRepository, ICustomersRepository customersRepository, IMapper mapper)
        {
            _serviceOrderRepository = serviceOrderRepository;
            _customersRepository = customersRepository;
            _mapper = mapper;
        }

        public async Task<List<ServiceOrderDto>> GetAllAsync()
        {
            List<ServiceOrder> serviceOrders = await _serviceOrderRepository.GetAllAsync();
            var serviceOrderDto = _mapper.Map<List<ServiceOrderDto>>(serviceOrders);
            return serviceOrderDto;
        }

        public async Task<ServiceOrderDto> GetServiceOrderAsync(int id)
        {
            ServiceOrder serviceOrder = await _serviceOrderRepository.GetByIdAsync(id);
            if (serviceOrder is null)
            {
                throw new NotFoundException("Ordem de serviço não encontrada");
            }
            var serviceOrderDto = _mapper.Map<ServiceOrderDto>(serviceOrder);
            return serviceOrderDto;
        }

        public async Task<ServiceOrderDto> GetServiceOrderFromUserAsync(int serviceOrderId)
        {
            ServiceOrder os = await _serviceOrderRepository.GetServiceOrderFromUser(serviceOrderId);

            if (os == null)
            {
                throw new Exception("Ordem de serviço não encontrada");
            }
            var serviceOrderDto = _mapper.Map<ServiceOrderDto>(os);
            return serviceOrderDto;
        }

        public async Task<ServiceOrderDto> GetServiceOrderWithComments(int serviceOrderId)
        {
            ServiceOrder serviceOrderWithComments = await _serviceOrderRepository.GetServiceOrderWithComments(serviceOrderId);
            var serviceOrder = _mapper.Map<ServiceOrderDto>(serviceOrderWithComments);
            return serviceOrder;
        }

        public async Task<NewServiceOrderDto> CreateServiceOrderAsync(CreateServiceOrderDto createServiceOrderDto)
        {
            if (createServiceOrderDto is null)
            {
                throw new BadRequest("Ordem de serviço não pode estar vazia");
            }

            Customer customer = await _customersRepository.GetByIdAsync(createServiceOrderDto.CustomerId);

            if (customer is null)
            {
                throw new BadRequest("A ordem de serviço não pode ser vinculada a um cliente desconhecido");
            }

            ServiceOrder serviceOrder = _mapper.Map<ServiceOrder>(createServiceOrderDto);
            await _serviceOrderRepository.AddAsync(serviceOrder);

            return _mapper.Map<NewServiceOrderDto>(serviceOrder);
        }

        public async Task FinishServiceOrderAsync(int id)
        {
            ServiceOrder serviceOrder = await _serviceOrderRepository.GetByIdAsync(id);

            if (serviceOrder is null)
            {
                throw new NotFoundException("Ordem de serviço não encontrada");
            }

            serviceOrder.FinishOS();
            await _serviceOrderRepository.FinishAsync(serviceOrder);
        }

        public async Task CancelServiceOrderAsync(int id)
        {
            ServiceOrder serviceOrder = await _serviceOrderRepository.GetByIdAsync(id);

            if (serviceOrder is null)
            {
                throw new NotFoundException("Ordem de serviço não encontrada");
            }

            serviceOrder.Cancel();
            await _serviceOrderRepository.CancelAsync(serviceOrder);
        }
    }
}
