using Domain.Entities.Ortak;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        //sadece get yapmamızın nedeni datamızın geleceği ama üzerinden değişiklik yapmayacağımızdan dolayı
        DbSet<T> Table { get; }
    }
}
