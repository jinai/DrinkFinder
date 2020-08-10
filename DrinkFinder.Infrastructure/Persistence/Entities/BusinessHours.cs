using DrinkFinder.Common.Enums;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    public class BusinessHours : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public IsoDay Day { get; set; }
        public TimeSpan? OpeningHour { get; set; }
        public TimeSpan? ClosingHour { get; set; }
        public DateTimeOffset AddedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public Establishment Establishment { get; set; }
    }
}