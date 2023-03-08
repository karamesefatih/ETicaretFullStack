using Application.RequestParameters;
using MediatR;

namespace Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        // public Pagination Pagination { get; set; }
        public int Page { get; set; } = 0;
        public int size { get; set; } = 5;
    }
}
