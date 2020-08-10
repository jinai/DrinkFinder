using DrinkFinder.Common.Enums;
using DrinkFinder.Infrastructure.Persistence.Entities;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using System;
using System.Linq;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public class EstablishmentRepository : RepositoryBase<Establishment, Guid>, IEstablishmentRepository
    {
        public EstablishmentRepository(DrinkFinderDomainContext context) : base(context) { }

        public IQueryable<Establishment> GetAllOpenOn(IsoDay day)
        {
            return GetWhere(e => e.BusinessHours.Any(bh => bh.Day == day && bh.OpeningHour != null));
        }

        public IQueryable<Establishment> GetAllByUserId(Guid userId)
        {
            return GetWhere(e => e.UserId == userId);
        }
    }
}
