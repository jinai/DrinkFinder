using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Entities;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public class BusinessHoursRepository : AsyncRepository<BusinessHours, Guid>, IBusinessHoursRepository
    {
        public BusinessHoursRepository(DrinkFinderDomainContext context) : base(context)
        {
        }
    }
}
