using DrinkFinder.Infrastructure.Persistence.Entities;
using System;
using System.Linq;

namespace DrinkFinder.Infrastructure.Persistence.Interfaces
{
    public interface IPictureRepository : IRepository<Picture, Guid>
    {
        public IQueryable<Picture> GetAllByEstablishmentId(Guid establishmentId);
    }
}
