using System;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEstablishmentRepository EstablishmentRepo { get; }
        INewsRepository NewsRepo { get; }
        IPictureRepository PictureRepo { get; }
        IBusinessHoursRepository BusinessHoursRepo { get; }

        Task SaveAsync();
    }
}
