using DrinkFinder.Infrastructure.Persistence.Entities;
using System;
using System.Linq;

namespace DrinkFinder.Infrastructure.Persistence.Interfaces
{
    public interface INewsRepository : IRepository<News, Guid>
    {
        public IQueryable<News> GetAllByEstablishmentId(Guid establishmentId);
    }
}
