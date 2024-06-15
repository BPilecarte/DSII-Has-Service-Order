using AutoMapper;
using OsDsII.Api.Dtos.Customers;
using OsDsII.Api.Models;
using OsDsII.Api.Repository.CustomersRepository;
using OsDsII.Api.Exceptions;

namespace OsDsII.Api.Services.Customers
{
    public sealed class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;

        public CustomersService(ICustomersRepository customersRepository, IMapper mapper)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            IEnumerable<Customer> customers = await _customersRepository.GetAllAsync();
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return customersDto;
        }

        public async Task<CustomerDto> GetCustomerAsync(int id)
        {
            Customer customer = await _customersRepository.GetByIdAsync(id);
            if (customer is null)
            {
                throw new NotFoundException("Cliente não encontrado");
            }
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }

        public async Task CreateAsync(CreateCustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            var customerExists = await _customersRepository.FindUserByEmailAsync(customer.Email);
            if (customerExists != null && !customerExists.Equals(customer))
            {
                throw new ConflictException("Cliente já existe");
            }

            await _customersRepository.AddCustomerAsync(customer);
        }

        public async Task UpdateAsync(int id, CreateCustomerDto customer)
        {
            Customer customerExists = await _customersRepository.GetByIdAsync(id);
            if (customerExists is null)
            {
                throw new NotFoundException("Cliente não encontrado");
            }
            customerExists.Email = customer.Email;
            customerExists.Name = customer.Name;
            customerExists.Phone = customer.Phone;

            await _customersRepository.UpdateCustomerAsync(customerExists);
        }

        public async Task DeleteAsync(int id)
        {
            Customer currentCustomer = await _customersRepository.GetByIdAsync(id);
            if (currentCustomer is null)
            {
                throw new NotFoundException("Cliente não encontrado");
            }
            await _customersRepository.DeleteCustomer(currentCustomer);
        }

    }
}