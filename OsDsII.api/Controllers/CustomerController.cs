using AutoMapper;
using OsDsII.Api.Dtos.Customers;
using OsDsII.Api.Exceptions;
using OsDsII.Api.Models;
using OsDsII.Api.Repository.CustomersRepository;
using OsDsII.Api.Services.Customers;
using Microsoft.AspNetCore.Mvc;

namespace OsDsII.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersRepository customersRepository, IMapper mapper, ICustomersService customersService)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
            _customersService = customersService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                IEnumerable<CustomerDto> customers = await _customersService.GetAllAsync();
                return Ok(customers);
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                CustomerDto customer = await _customersService.GetCustomerAsync(id);
                return Ok(customer);
            }
            catch (BaseException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomerAsync(CreateCustomerDto customer)
        {
            try
            {
                await _customersService.CreateAsync(customer);
                return Created(nameof(CustomersController), customer);
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            try
            {
                await _customersService.DeleteAsync(id);
                return NoContent();
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] CreateCustomerDto customer, int id)
        {
            try
            {
                await _customersService.UpdateAsync(id, customer);
                return NoContent();
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }
    }
}