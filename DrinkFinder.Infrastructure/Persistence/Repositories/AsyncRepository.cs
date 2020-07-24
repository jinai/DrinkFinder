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

        public async Task<TEntity> GetById(TId id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var added = await dbSet.AddAsync(entity);
            return added.Entity;
        }

        public Task<TEntity> Update(TEntity entity)
        {
            var updated = context.Attach<TEntity>(entity);
            updated.State = EntityState.Modified;
            return Task.FromResult(updated.Entity);
        }

        public async Task<TEntity> Remove(TId id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            return await Remove(entityToDelete);
        }

        public Task<TEntity> Remove(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            return Task.FromResult(dbSet.Remove(entity).Entity);
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
