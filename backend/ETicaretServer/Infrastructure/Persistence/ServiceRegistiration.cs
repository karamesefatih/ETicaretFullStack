using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence
{
    //Onion architecture da IoC yapılandırmasını burada kullanacağız
    public static class ServiceRegistiration
    {

        public static void AddPersistenceServices(this IServiceCollection services)
        {
            //birden fazla dependency injection yapabilmek için bircontrollerda ServiceLifetime.Singleton kullandık
            services.AddDbContext<ETicaretDbContext>(options => options.UseNpgsql(Configuration.ConnectionString),ServiceLifetime.Singleton);
            //servislerimizin dependency injectionunu yaptık
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRespository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}
