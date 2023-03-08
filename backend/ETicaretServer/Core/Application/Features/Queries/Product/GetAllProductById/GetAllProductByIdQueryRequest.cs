using MediatR;

namespace Application.Features.Queries.Product.GetAllProductById
{
    public class GetAllProductByIdQueryRequest : IRequest<GetAllProductByIdQueryResponse>
    {
        public string Id { get; set; }  
    }
}
