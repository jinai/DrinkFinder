using DrinkFinder.Api.Helpers;

namespace DrinkFinder.Api.ResourceParameters
{
    public abstract class BaseParameters : IPaginator
    {
        public const int minPageIndex = 1;
        public const int minPageSize = 1;
        public const int maxPageSize = 50;

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
