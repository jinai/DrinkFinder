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
        internal DrinkFinderContext context;
        internal DbSet<TEntity> dbSet;

        public AsyncRepository(DrinkFinderContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async ValueTask<TEntity> GetById(TId id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async ValueTask<TEntity> Add(TEntity entity)
        {
            var added = await dbSet.AddAsync(entity);
            return added.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            var updated = context.Attach<TEntity>(entity);
            updated.State = EntityState.Modified;
            return updated.Entity;
        }

        public async ValueTask<bool> Remove(TId id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            return Remove(entityToDelete);
        }

        public bool Remove(TEntity entity)
        {
            entity.IsDeleted = true;
            var removed = Update(entity);
            return removed.IsDeleted;
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                predicate = _ => true;
            }
            return await dbSet.CountAsync(predicate);
        }
    }
}
