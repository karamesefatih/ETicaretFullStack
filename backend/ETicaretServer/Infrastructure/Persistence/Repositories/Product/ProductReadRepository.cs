using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRespository
    {
        public ProductReadRepository(ETicaretDbContext context) : base(context)
        {
        }
    }
}
