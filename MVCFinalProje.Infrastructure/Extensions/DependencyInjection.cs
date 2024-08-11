using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCFinalProje.Infrastructure.AppContext;
using MVCFinalProje.Infrastructure.Repositories.AuthorRepositories;
using MVCFinalProje.Infrastructure.Repositories.BookRepository;
using MVCFinalProje.Infrastructure.Repositories.CustomerRepository;
using MVCFinalProje.Infrastructure.Repositories.PublisherRepository;
using MVCFinalProje.Infrastructure.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString("AppConnecitonString"));
            });
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddHttpContextAccessor();






            // Genelde migration işlemlerinde yoruma almak zorunda kalabiliriz.

            AdminSeed.SeedAsync(configuration).GetAwaiter().GetResult();

            return services;
        }
    }
}
