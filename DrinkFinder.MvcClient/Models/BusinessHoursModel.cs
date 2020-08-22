using DrinkFinder.Common.Enums;
using System;

namespace DrinkFinder.MvcClient.Models
{
    public class BusinessHoursModel
    {
        public IsoDay Day { get; set; }
        public TimeSpan? OpeningHour { get; set; }
        public TimeSpan? ClosingHour { get; set; }

        public override string ToString()
        {
            if (OpeningHour == default || ClosingHour == default)
            {
                return "Closed";
            }

            var openingHour = DateTime.Today.Add(OpeningHour.Value);
            var closingHour = DateTime.Today.Add(ClosingHour.Value);
            return $"{openingHour:hh\\:mm tt} - {closingHour:hh\\:mm tt}";
        }
    }
}
