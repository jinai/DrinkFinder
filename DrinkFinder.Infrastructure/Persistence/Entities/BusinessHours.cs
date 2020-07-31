using DrinkFinder.Common.Enums;
using DrinkFinder.Common.Interfaces;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    public class BusinessHours : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Day Day { get; set; }
        public TimeSpan? OpeningHour { get; set; }
        public TimeSpan? ClosingHour { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public Establishment Establishment { get; set; }
    }
}