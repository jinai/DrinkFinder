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
        IBusinessHoursRepository BusinessHoursRepo { get; }

        Task SaveAsync();
    }
}
