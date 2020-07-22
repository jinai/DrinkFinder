using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DrinkFinder.Infrastructure.Persistence.Context
{
    public class DrinkFinderContextFactory : IDesignTimeDbContextFactory<DrinkFinderContext>
    {
        public DrinkFinderContext CreateDbContext(string[] args)
        {
            //var configBuilder = new ConfigurationBuilder();
            //configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            //configBuilder.AddJsonFile("appsettings.json");
            //var config = configBuilder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<DrinkFinderContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DrinkFinder;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new DrinkFinderContext(optionsBuilder.Options);
        }
    }
}
