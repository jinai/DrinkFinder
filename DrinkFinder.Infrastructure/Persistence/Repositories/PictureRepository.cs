using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Entities;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public class PictureRepository : AsyncRepository<Picture, Guid>, IPictureRepository
    {
        public PictureRepository(DrinkFinderDomainContext context) : base(context)
        {
        }
    }
}
