using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrinkFinder.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DrinkFinderContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DrinkFinderConnection"),
                        b => b.MigrationsAssembly(typeof(DrinkFinderContext).Assembly.FullName)));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DrinkFinderContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
