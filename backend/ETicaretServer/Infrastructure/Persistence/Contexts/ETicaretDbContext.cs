using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts
{
    //DbContext Veritabanına karşılık gelen sınıftır
    public class ETicaretDbContext : DbContext
    {
        //Bu constructer IOC de doldurulacaktır
        public ETicaretDbContext(DbContextOptions options) : base(options)
        {
        }
        //Veritabanını temsil eden bu contextte sanki bu isimde bir tablo olacakmış gibi gösterdim
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }
}
