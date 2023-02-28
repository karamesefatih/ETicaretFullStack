using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction
{
    //bu interface'i implemente eden sınıfları veritabanı ile alaklıysa persistence,değilse infrastructure da oluşturulacak 
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
