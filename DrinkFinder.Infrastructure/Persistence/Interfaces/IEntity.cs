using System;

namespace DrinkFinder.Infrastructure.Persistence.Interfaces
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
        public DateTimeOffset AddedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
