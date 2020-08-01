using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Identity;
using DrinkFinder.Infrastructure.Persistence.Repositories;
using DrinkFinder.Infrastructure.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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

            services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<DrinkFinderContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
