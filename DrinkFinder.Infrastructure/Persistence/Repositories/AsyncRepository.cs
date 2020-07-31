using DrinkFinder.Common.Interfaces;
using DrinkFinder.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public class AsyncRepository<TEntity, TId> : IAsyncRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        private readonly DrinkFinderContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public AsyncRepository(DrinkFinderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> GetById(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var added = await _dbSet.AddAsync(entity);
            return added.Entity;
        }

        public Task<TEntity> Update(TEntity entity)
        {
            var updated = _context.Attach<TEntity>(entity);
            updated.State = EntityState.Modified;
            return Task.FromResult(updated.Entity);
        }

        public async Task<TEntity> Remove(TId id)
        {
            TEntity entityToDelete = await _dbSet.FindAsync(id);
            return await Remove(entityToDelete);
        }

        public Task<TEntity> Remove(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            return Task.FromResult(_dbSet.Remove(entity).Entity);
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                predicate = _ => true;
            }
            return await _dbSet.CountAsync(predicate);
        }
    }
}
