using DrinkFinder.Common.Interfaces;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    public class Picture : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Uri Location { get; set; }
        public DateTimeOffset AddedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public Establishment Establishment { get; set; }
    }
}