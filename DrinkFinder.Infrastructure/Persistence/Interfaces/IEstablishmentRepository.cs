using DrinkFinder.Common.Enums;
using DrinkFinder.Infrastructure.Persistence.Entities;
using System;
using System.Linq;

namespace DrinkFinder.Infrastructure.Persistence.Interfaces
{
    public interface IEstablishmentRepository : IRepository<Establishment, Guid>
    {
        public IQueryable<Establishment> GetAllOpenOn(IsoDay day);
        public IQueryable<Establishment> GetAllByUserId(Guid userId);
    }
}
