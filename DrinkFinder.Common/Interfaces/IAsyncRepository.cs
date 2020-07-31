using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DrinkFinder.Common.Interfaces
{
    public interface IAsyncRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        Task<TEntity> GetById(TId id);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Remove(TId id);
        Task<TEntity> Remove(TEntity entity);

        Task<int> Count(Expression<Func<TEntity, bool>> predicate = null);
    }
}
