using AutoMapper;
using Moq;
using OsDsII.Api.Dtos.Customers;
using OsDsII.Api.Models;
using OsDsII.Api.Repository.CustomersRepository;
using OsDsII.Api.Services.Customers;

namespace HasServiceOrder.Tests.Service
{
    public class CustomersServiceTests
    {
        private readonly Mock<ICustomersRepository> _mockCustomersRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CustomersService _service;

        public CustomersServiceTests()
        {
            _mockCustomersRepository = new Mock<ICustomersRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new CustomersService(_mockCustomersRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Should_Return_A_List_Of_Customers()
        {
            List<Customer> customers = new List<Customer>()
            {
                new Customer(1, "Nathalli", "nathalli@email.com", "123456789"),
                new Customer(2, "Beatriz", "beatriz@email.com", "987654321"),
            };

            List<CustomerDto> customersDto = new List<CustomerDto>()
            {
                new CustomerDto("Nathalli", "nathalli@email.com", "912312312", null),
                new CustomerDto("Beatriz", "beatriz@email.com", "932132132", null),
            };

            _mockCustomersRepository.Setup(repository => repository.GetAllAsync()).ReturnsAsync(customers);
            var result = await _service.GetAllAsync();
            Assert.Equal(customersDto, result);
        }
    }
}
