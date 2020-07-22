using DrinkFinder.Infrastructure.Persistence.Abstract;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    public class Timetable : BaseEntity
    {
        public TimeSpan OpeningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }

        public Establishment Establishment { get; set; }
    }
}