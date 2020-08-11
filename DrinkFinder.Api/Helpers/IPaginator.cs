namespace DrinkFinder.Api.Helpers
{
    public interface IPaginator
    {
        int PageIndex { get; }
        int PageSize { get; }
    }
}
