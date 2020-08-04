using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Repositories;
using System;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DrinkFinderDomainContext _context;

        public UnitOfWork(DrinkFinderDomainContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private IEstablishmentRepository _establishmentRepo;
        private INewsRepository _newsRepo;
        private IPictureRepository _pictureRepo;
        private IBusinessHoursRepository _businessHoursRepo;

        public IEstablishmentRepository EstablishmentRepo
        {
            get { return _establishmentRepo ??= new EstablishmentRepository(_context); }
        }

        public INewsRepository NewsRepo
        {
            get { return _newsRepo ??= new NewsRepository(_context); }
        }

        public IPictureRepository PictureRepo
        {
            get { return _pictureRepo ??= new PictureRepository(_context); }
        }

        public IBusinessHoursRepository BusinessHoursRepo
        {
            get { return _businessHoursRepo ??= new BusinessHoursRepository(_context); }
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
