using DrinkFinder.Infrastructure.Persistence.Entities;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using System;
using System.Linq;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public class PictureRepository : RepositoryBase<Picture, Guid>, IPictureRepository
    {
        public PictureRepository(DrinkFinderDomainContext context) : base(context) { }

        public IQueryable<Picture> GetAllByEstablishmentId(Guid establishmentId)
        {
            return GetWhere(p => p.Establishment.Id == establishmentId);
        }
    }
}
