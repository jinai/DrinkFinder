using System;

namespace DrinkFinder.Common.Interfaces
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
        public DateTimeOffset AddedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
