using Application.Features.Commands.Product.CreateProduct;
using Application.Features.Commands.Product.RemoveProduct;
using Application.Features.Commands.Product.UpdateProduct;
using Application.Features.Queries.Product.GetAllProduct;
using Application.Features.Queries.Product.GetAllProductById;
using Application.Repositories;
using Application.RequestParameters;
using Application.Services;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    //Controllerlar entity ile karşılanmamalıdır View Modellar ile yapılır yada CQRS pattern ile
    [Route("api/[controller]")]
    [ApiController] 
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRespository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly IMediator _mediator;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRespository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService, IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            //Mediatr ile handler üzerinden response döndük
            GetAllProductQueryResponse response =  await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);

        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> Get([FromRoute] GetAllProductByIdQueryRequest request)
        {
            GetAllProductByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);

        }
        [HttpPost]
        public async Task<IActionResult> post(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response =  await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> put(UpdateProductCommandRequest request)
        {
            UpdateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);

        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
        {
            RemoveProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
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
