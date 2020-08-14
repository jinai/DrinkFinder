using DrinkFinder.Infrastructure.Persistence.Entities;
using System;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Establishment, Guid> EstablishmentRepo { get; }
        IRepository<News, Guid> NewsRepo { get; }
        IRepository<Picture, Guid> PictureRepo { get; }
        IRepository<BusinessHours, Guid> BusinessHoursRepo { get; }

        Task SaveAsync();
    }
}
