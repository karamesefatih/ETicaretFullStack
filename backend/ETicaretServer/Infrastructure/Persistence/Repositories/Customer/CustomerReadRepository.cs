using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//namespace'i yeniden adlandıırdık
namespace Persistence.Repositories
{
    //customer okuma crud işlemleri burada yapılacak ıcustomerdan miras alıyoruz aynı zamanda read repositoryden de soyutlama yapıyoruz generic olarak
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(ETicaretDbContext context) : base(context)
        {
        }
    }
}
