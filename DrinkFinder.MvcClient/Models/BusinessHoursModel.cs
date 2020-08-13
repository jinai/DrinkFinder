using DrinkFinder.Common.Enums;
using System;

namespace DrinkFinder.MvcClient.Models
{
    public class BusinessHoursModel
    {
        public IsoDay Day { get; set; }
        public TimeSpan OpeningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }
    }
}
