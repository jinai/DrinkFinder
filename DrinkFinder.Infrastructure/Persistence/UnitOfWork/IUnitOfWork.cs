using DrinkFinder.Infrastructure.Persistence.Repositories;
using System;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEstablishmentRepository EstablishmentRepo { get; }
        INewsRepository NewsRepo { get; }
        IPhotoRepository PhotoRepo { get; }
        ITimetableRepository TimetableRepo { get; }

        Task SaveAsync();
    }
}
