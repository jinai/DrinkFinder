using DrinkFinder.Common.Interfaces;
using DrinkFinder.Infrastructure.Persistence.Entities;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public interface IPhotoRepository : IAsyncRepository<Photo, Guid>
    {
    }
}
