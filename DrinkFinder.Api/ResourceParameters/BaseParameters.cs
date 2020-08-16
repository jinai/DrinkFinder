using DrinkFinder.Infrastructure.Persistence.Helpers;

namespace DrinkFinder.Api.ResourceParameters
{
    public abstract class BaseParameters : IPaginator
    {
        public const int MinPageIndex = 1;
        public const int MinPageSize = 1;
        public const int MaxPageSize = 50;

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
