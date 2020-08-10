using DrinkFinder.Infrastructure.Persistence.Entities;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using System;
using System.Linq;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public class BusinessHoursRepository : RepositoryBase<BusinessHours, Guid>, IBusinessHoursRepository
    {
        public BusinessHoursRepository(DrinkFinderDomainContext context) : base(context) { }

        public IQueryable<BusinessHours> GetAllByEstablishmentId(Guid establishmentId)
        {
            return GetWhere(bh => bh.Establishment.Id == establishmentId);
        }
    }
}
