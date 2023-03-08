using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Product.GetAllProductById
{
    public class GetAllProductByIdQueryResponse
    {
        public object Products { get; set; }
        public string Message { get; set; }
    }
}
