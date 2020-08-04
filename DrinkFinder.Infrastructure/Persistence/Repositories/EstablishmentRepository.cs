using DrinkFinder.Infrastructure.Persistence.Context;
using DrinkFinder.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Persistence.Repositories
{
    public class EstablishmentRepository : AsyncRepository<Establishment, Guid>, IEstablishmentRepository
    {
        public EstablishmentRepository(DrinkFinderDomainContext context) : base(context) { }

        public override async Task<IEnumerable<Establishment>> GetAll(Expression<Func<Establishment, bool>> predicate = null, Func<IQueryable<Establishment>, IOrderedQueryable<Establishment>> orderBy = null)
        {
            IQueryable<Establishment> query = DbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = query.Include(e => e.BusinessHours)
                         .Include(e => e.News)
                         .Include(e => e.Pictures);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public override async Task<Establishment> FirstOrDefault(Expression<Func<Establishment, bool>> predicate)
        {
            var query = DbSet.Include(e => e.BusinessHours)
                              .Include(e => e.News)
                              .Include(e => e.Pictures);
            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}
