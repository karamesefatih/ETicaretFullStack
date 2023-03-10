using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.Ortak;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts
{
    //DbContext Veritabanına karşılık gelen sınıftır
    public class ETicaretDbContext : IdentityDbContext<User,Role,string>
    {
        //Bu constructer IOC de doldurulacaktır
        public ETicaretDbContext(DbContextOptions options) : base(options)
        {
        }
        //Veritabanını temsil eden bu contextte sanki bu isimde bir tablo olacakmış gibi gösterdim
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //Change Tracker => Entityler üzerinden yapılan değişikliklerin yakalanmasını sağlar
            //Base entityde manipülasyon yapıyoruz
            var datas = ChangeTracker.Entries<BaseEntity>();
            //ne kadar data geldiğini bilemeyeceğimiz için dögüye soktuk
            foreach(var data in datas)
            {
                //datayı boşyere tutmamak için discard ettik
                switch ( data.State) 
                {
                    case EntityState.Added:
                        data.Entity.CreatedDate= DateTime.UtcNow.AddHours(3);
                        data.Entity.LastUpdateDate = DateTime.UtcNow.AddHours(3);
                        break;
                    case EntityState.Modified:
                        data.Entity.LastUpdateDate = DateTime.UtcNow.AddHours(3);
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
         }

    }
}
