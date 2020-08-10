using DrinkFinder.Infrastructure.Persistence.Entities;
using System;
using System.Linq;

namespace DrinkFinder.Infrastructure.Persistence.Interfaces
{
    public interface IBusinessHoursRepository : IRepository<BusinessHours, Guid>
    {
        public IQueryable<BusinessHours> GetAllByEstablishmentId(Guid establishmentId);
    }
}
