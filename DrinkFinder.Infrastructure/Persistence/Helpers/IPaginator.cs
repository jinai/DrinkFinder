namespace DrinkFinder.Infrastructure.Persistence.Helpers
{
    public interface IPaginator
    {
        int PageIndex { get; }
        int PageSize { get; }
    }
}
