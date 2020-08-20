using DrinkFinder.Infrastructure.Persistence;
using DrinkFinder.Infrastructure.Persistence.Entities;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DrinkFinder.Api
{
    public static class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            if (connectionString is null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            // Check if seed file exists first
            var seedFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "entities_seed.json");
            if (!File.Exists(seedFile))
            {
                return;
            }

            var services = new ServiceCollection();
            services.AddDbContext<DrinkFinderDomainContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DrinkFinderDomainContext>();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            context.Database.Migrate();

            // If an establishment with that ShortCode exists we can assume we've already seeded the database
            var control = uow.EstablishmentRepo.GetWhere(e => e.ShortCode == "delirium").SingleOrDefaultAsync().Result;
            if (control != null)
            {
                return;
            }

            // Deserialize seed file
            var json = File.ReadAllText(seedFile, Encoding.UTF8);
            List<Establishment> establishments = JsonConvert.DeserializeObject<List<Establishment>>(json);

            // Set IDs and AddedDates if necessary
            foreach (var estab in establishments)
            {
                if (estab.Id == default) { estab.Id = Guid.NewGuid(); }
                if (estab.AddedDate == default) { estab.AddedDate = DateTimeOffset.Now; }

                foreach (var bh in estab.BusinessHours)
                {
                    if (bh.Id == default) { bh.Id = Guid.NewGuid(); }
                    if (bh.AddedDate == default) { bh.AddedDate = DateTimeOffset.Now; }
                }

                foreach (var pic in estab.Pictures)
                {
                    if (pic.Id == default) { pic.Id = Guid.NewGuid(); }
                    if (pic.AddedDate == default) { pic.AddedDate = DateTimeOffset.Now; }
                }

                foreach (var news in estab.News)
                {
                    if (news.Id == default) { news.Id = Guid.NewGuid(); }
                    if (news.AddedDate == default) { news.AddedDate = DateTimeOffset.Now; }
                    if (news.UserId == default) { news.UserId = estab.UserId; }
                }
            }

            context.Establishments.AddRange(establishments);
            context.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
