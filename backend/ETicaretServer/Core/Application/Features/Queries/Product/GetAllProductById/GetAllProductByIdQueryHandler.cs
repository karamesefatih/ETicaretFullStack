using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Product.GetAllProductById
{
    public class GetAllProductByIdQueryHandler : IRequestHandler<GetAllProductByIdQueryRequest, GetAllProductByIdQueryResponse>
    {
        private readonly IProductReadRespository _productReadRespository;

        public GetAllProductByIdQueryHandler(IProductReadRespository productReadRespository)
        {
            _productReadRespository = productReadRespository;
        }

        public async Task<GetAllProductByIdQueryResponse> Handle(GetAllProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productReadRespository.GetByIdAsync(request.Id, false);
            return new()
            {
                Products= products,
                Message = "Product Has Been Get By Id"
            };
        }
    }
}
