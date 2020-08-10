﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Persistence.Interfaces
{
    public interface IRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetById(TId id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(TId id);
    }
}