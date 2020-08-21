using DrinkFinder.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DrinkFinder.Infrastructure.Persistence
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IEntity<TId>, new()
                                                                      where TId : IEquatable<TId>
    {
        private readonly DrinkFinderDomainContext _context;
        private readonly DbSet<TEntity> _set;

        public Repository(DrinkFinderDomainContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _set.AsNoTracking();
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return _set.Where(predicate).AsNoTracking();
        }

        public IQueryable<TEntity> GetById(TId id)
        {
            return GetWhere(entity => entity.Id.Equals(id));
        }

        public void Add(TEntity entity)
        {
            _set.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _set.Attach(entity);
            }

            _set.Remove(entity);
        }

        public void Remove(TId id)
        {
            var entity = new TEntity() { Id = id };
            Remove(entity);
        }
    }
}
