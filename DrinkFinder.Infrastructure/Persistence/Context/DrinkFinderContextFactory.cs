using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DrinkFinder.Infrastructure.Persistence.Context
{
    public class DrinkFinderContextFactory : IDesignTimeDbContextFactory<DrinkFinderContext>
    {
        public DrinkFinderContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DrinkFinderContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DrinkFinder;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new DrinkFinderContext(optionsBuilder.Options);
        }
    }
}
