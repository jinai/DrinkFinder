namespace DrinkFinder.Common.Interfaces
{
    public interface IEntity<TIdType>
    {
        TIdType Id { get; set; }
    }
}
