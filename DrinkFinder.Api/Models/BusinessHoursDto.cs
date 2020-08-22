using DrinkFinder.Common.Enums;
using System;

namespace DrinkFinder.Api.Models
{
    public class BusinessHoursDto
    {
        public IsoDay Day { get; set; }
        public TimeSpan? OpeningHour { get; set; }
        public TimeSpan? ClosingHour { get; set; }
    }
}
