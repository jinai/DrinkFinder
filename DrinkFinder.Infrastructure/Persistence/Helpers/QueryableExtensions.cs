using System;
using System.Linq;

namespace DrinkFinder.Infrastructure.Persistence.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, IPaginator paginator)
        {
            if (paginator is null)
            {
                throw new ArgumentNullException(nameof(paginator));
            }

            return source.Skip((paginator.PageIndex - 1) * paginator.PageSize).Take(paginator.PageSize);
        }
    }
}
