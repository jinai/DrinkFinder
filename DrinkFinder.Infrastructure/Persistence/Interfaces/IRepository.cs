using System;
using System.Linq;
using System.Linq.Expressions;

namespace DrinkFinder.Infrastructure.Persistence.Interfaces
{
    public interface IRepository<TEntity, in TId> where TEntity : IEntity<TId>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetById(TId id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(TId id);
    }
}
