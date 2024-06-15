using AutoMapper;
using OsDsII.Api.Dtos;
using OsDsII.Api.Dtos.Customers;
using OsDsII.Api.Dtos.ServiceOrders;
using OsDsII.Api.Models;

namespace OsDsII.api
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<Customer, CustomerDto>();

            CreateMap<ServiceOrder, CreateServiceOrderDto>();
            CreateMap<CreateServiceOrderDto, ServiceOrder>();
            CreateMap<ServiceOrder, NewServiceOrderDto>();
            CreateMap<ServiceOrder, ServiceOrderDto>();
        }
    }
}
