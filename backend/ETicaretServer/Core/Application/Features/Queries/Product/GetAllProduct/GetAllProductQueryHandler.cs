using Application.Repositories;
using MediatR;

namespace Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRespository _productReadRespository;

        public GetAllProductQueryHandler(IProductReadRespository productReadRespository)
        {
            _productReadRespository = productReadRespository;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _productReadRespository.GetAll(false).Count();
            var products = _productReadRespository.GetAll(false).Skip(request.Page * request.size).Take(request.size).ToList();

            return new()
            {
                Products = products,
                TotalCount = totalCount,
            };
        }
    }
}
