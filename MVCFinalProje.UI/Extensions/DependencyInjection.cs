using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity;
using MVCFinalProje.Infrastructure.AppContext;

namespace MVCFinalProje.UI.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUIServices(this IServiceCollection services)
        {
            services.AddNotyf(config =>
            {
                config.HasRippleEffect = true;
                config.DurationInSeconds = 3;
                config.Position = NotyfPosition.BottomRight;
                config.IsDismissable = true;
            });




            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}
