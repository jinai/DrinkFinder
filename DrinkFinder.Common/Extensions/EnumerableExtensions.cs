using System.Collections.Generic;
using System.Linq;

namespace DrinkFinder.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<(int index, T item)> Enumerated<T>(this IEnumerable<T> self)
        {
            return self?.Select((item, index) => (index, item)) ?? new List<(int, T)>();
        }
    }
}
