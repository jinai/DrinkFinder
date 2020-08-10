using DrinkFinder.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public abstract class RepositoryBase<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IEntity<TId>, new()
                                                                                   where TId : IEquatable<TId>
    {
        private readonly DrinkFinderDomainContext _context;
        protected DbSet<TEntity> Set { get; }

        public RepositoryBase(DrinkFinderDomainContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Set = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return Set.AsNoTracking();
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return Set.Where(predicate).AsNoTracking();
        }

        public Task<TEntity> GetById(TId id)
        {
            return Set.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public void Add(TEntity entity)
        {
            Set.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                Set.Attach(entity);
            }
            Set.Remove(entity);
        }

        public void Remove(TId id)
        {
            TEntity entity = new TEntity() { Id = id };
            Remove(entity);
        }
    }
}
