using Microsoft.AspNetCore.Identity;
using MVCFinalProje.Infrastructure.AppContext;

namespace MVCFinalProje.API.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
          
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}
