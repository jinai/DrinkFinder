using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DrinkFinder.Common.Interfaces
{
    public interface IAsyncRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        ValueTask<TEntity> GetById(TId id);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate = null);

        ValueTask<TEntity> Add(TEntity entity);
        TEntity Update(TEntity entity);
        ValueTask<bool> Remove(TId id);
        bool Remove(TEntity entity);

        Task<int> Count(Expression<Func<TEntity, bool>> predicate = null);
    }
}
