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
                                                                                where TId : IEquatable<TId>
    {
        private readonly DrinkFinderContext _context;
        protected DbSet<TEntity> DbSet { get; }

        public AsyncRepository(DrinkFinderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> GetById(TId id)
        {
            return await FirstOrDefault(e => e.Id.Equals(id));
        }

        public virtual async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = DbSet;

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
            var added = await DbSet.AddAsync(entity);
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
            TEntity entityToDelete = await DbSet.FindAsync(id);
            return await Remove(entityToDelete);
        }

        public Task<TEntity> Remove(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            return Task.FromResult(DbSet.Remove(entity).Entity);
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                predicate = _ => true;
            }
            return await DbSet.CountAsync(predicate);
        }
    }
}
