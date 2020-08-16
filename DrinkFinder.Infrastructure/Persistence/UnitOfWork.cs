using DrinkFinder.Infrastructure.Persistence.Entities;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using System;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DrinkFinderDomainContext _context;

        public UnitOfWork(DrinkFinderDomainContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private IRepository<Establishment, Guid> _establishmentRepo;
        private IRepository<News, Guid> _newsRepo;
        private IRepository<Picture, Guid> _pictureRepo;
        private IRepository<BusinessHours, Guid> _businessHoursRepo;

        public IRepository<Establishment, Guid> EstablishmentRepo
        {
            get { return _establishmentRepo ??= new Repository<Establishment, Guid>(_context); }
        }

        public IRepository<News, Guid> NewsRepo
        {
            get { return _newsRepo ??= new Repository<News, Guid>(_context); }
        }

        public IRepository<Picture, Guid> PictureRepo
        {
            get { return _pictureRepo ??= new Repository<Picture, Guid>(_context); }
        }

        public IRepository<BusinessHours, Guid> BusinessHoursRepo
        {
            get { return _businessHoursRepo ??= new Repository<BusinessHours, Guid>(_context); }
        }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposedValue; // To detect redundant calls

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
