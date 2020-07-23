using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Entities;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public class NewsRepository : AsyncRepository<News, Guid>, INewsRepository
    {
        public NewsRepository(DrinkFinderContext context) : base(context)
        {
        }
    }
}
