using Microsoft.Extensions.DependencyInjection;
using MVCFinalProje.Business.Services.AccountServices;
using MVCFinalProje.Business.Services.AuthorServices;
using MVCFinalProje.Business.Services.BookServices;
using MVCFinalProje.Business.Services.CustomerServices;
using MVCFinalProje.Business.Services.PublisherServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}
