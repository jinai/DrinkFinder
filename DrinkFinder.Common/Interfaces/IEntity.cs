using System;

namespace DrinkFinder.Common.Interfaces
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
