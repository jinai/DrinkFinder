using DrinkFinder.Common.Enums;
using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Repositories;
using DrinkFinder.Infrastructure.Persistence.UnitOfWork;
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
                    b => b.MigrationsHistoryTable("__EFMigrationsHistory", nameof(Schema.Domain)));
                options.EnableSensitiveDataLogging();
            });

            services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
