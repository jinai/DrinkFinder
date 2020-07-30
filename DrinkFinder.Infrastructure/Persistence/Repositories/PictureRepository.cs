using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Entities;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public class PictureRepository : AsyncRepository<Picture, Guid>, IPictureRepository
    {
        public PictureRepository(DrinkFinderContext context) : base(context)
        {
        }
    }
}
