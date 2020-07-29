using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Repositories;
using System;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DrinkFinderContext _context;

        public UnitOfWork(DrinkFinderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private IEstablishmentRepository establishmentRepo;
        private INewsRepository newsRepo;
        private IPhotoRepository photoRepo;
        private IBusinessHoursRepository businessHoursRepo;

        public IEstablishmentRepository EstablishmentRepo
        {
            get { return establishmentRepo ??= new EstablishmentRepository(_context); }
        }

        public INewsRepository NewsRepo
        {
            get { return newsRepo ??= new NewsRepository(_context); }
        }

        public IPhotoRepository PhotoRepo
        {
            get { return photoRepo ??= new PhotoRepository(_context); }
        }

        public IBusinessHoursRepository BusinessHoursRepo
        {
            get { return businessHoursRepo ??= new BusinessHoursRepository(_context); }
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
