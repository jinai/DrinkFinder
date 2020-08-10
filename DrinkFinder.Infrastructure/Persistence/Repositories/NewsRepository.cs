using DrinkFinder.Infrastructure.Persistence.Entities;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using System;
using System.Linq;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public class NewsRepository : RepositoryBase<News, Guid>, INewsRepository
    {
        public NewsRepository(DrinkFinderDomainContext context) : base(context) { }

        public IQueryable<News> GetAllByEstablishmentId(Guid establishmentId)
        {
            return GetWhere(n => n.Establishment.Id == establishmentId);
        }
    }
}
