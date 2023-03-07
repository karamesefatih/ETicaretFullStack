using Application.Repositories;
using Application.RequestParameters;
using Application.Services;
using Application.ViewModels.Products;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    //Controllerlar entity ile karşılanmamalıdır View Modellar ile yapılır yada CQRS pattern ile
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRespository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRespository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }

        [HttpGet]

        public async Task<IActionResult> get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll().Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.size).Take(pagination.size).ToList();
            return Ok(new
            {
                totalCount,
                products
            });

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> get(string id)
        {

            return Ok(await _productReadRepository.GetByIdAsync(id, false));

        }
        [HttpPost]
        public async Task<IActionResult> post(VM_Create_Product product)
        {
            if (ModelState.IsValid)
            {

            }
            await _productWriteRepository.AddAsync(new()
            {
                ProductName = product.Name,
                Price = product.price,
                Stock = product.stock,
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> put(VM_Update_Product product)
        {
            Product products = await _productReadRepository.GetByIdAsync(product.Id);
            products.Stock = product.stock;
            products.ProductName = product.Name;
            products.Price = product.price;
            await _productWriteRepository.SaveAsync();
            return StatusCode(200);

        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return StatusCode(200);
        }
        //Dosya ekleme metodu
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            await _fileService.UploadAsync("resource/product-images",Request.Form.Files);
            return Ok();
        }
    }
}
