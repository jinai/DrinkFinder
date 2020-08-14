using DrinkFinder.Common.Constants;
using DrinkFinder.Infrastructure.Persistence;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrinkFinder.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DrinkFinderDomainContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DrinkFinderDomain"),
                    b => b.MigrationsHistoryTable("__EFMigrationsHistory", Schemas.Domain));
                options.EnableSensitiveDataLogging();
            });

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
