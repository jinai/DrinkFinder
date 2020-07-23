using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Entities;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public class PhotoRepository : AsyncRepository<Photo, Guid>, IPhotoRepository
    {
        public PhotoRepository(DrinkFinderContext context) : base(context)
        {
        }
    }
}
