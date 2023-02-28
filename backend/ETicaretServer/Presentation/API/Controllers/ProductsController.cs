using Application.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Persistence.Repositories;
using System.Configuration;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IProductReadRespository _productReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IOrderWriteRepository orderWriteRepository, IProductReadRespository productReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _orderWriteRepository = orderWriteRepository;
            _productReadRepository = productReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        [HttpGet]

        public async Task get()
        {
            var customerId = Guid.NewGuid();
            await _customerWriteRepository.AddAsync(new() { Id = customerId, Name = "Customer"});

            await _orderWriteRepository.AddAsync(new() { Description = "addadasadsdsd", Address = "Ankara",CustomerId = customerId});
            await _orderWriteRepository.AddAsync(new() { Description = "addadasadsdsd", Address = "bursa" , CustomerId = customerId});
            await _orderWriteRepository.AddAsync(new() { Description = "addadasadsdsd", Address = "istanbul" ,
                CustomerId = customerId
            });
            await _orderWriteRepository.SaveAsync();

        }
    }
}
