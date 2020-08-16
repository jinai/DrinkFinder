using DrinkFinder.Common.Constants;
using DrinkFinder.Infrastructure.Persistence;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using DrinkFinder.Infrastructure.ShortCode;
using DrinkFinder.Infrastructure.Vat;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DrinkFinder.Infrastructure
{
    public static class DependencyInjector
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

            services.AddScoped<IShortCodeService, ShortCodeService>();
            services.AddHttpClient<IVatService, VatService>(client =>
            {
                client.BaseAddress = new Uri(configuration["ExternalApis:vatlayer"]);
            });

            return services;
        }
    }
}
