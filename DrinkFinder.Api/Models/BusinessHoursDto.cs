using DrinkFinder.Common.Enums;
using System;

namespace DrinkFinder.Api.Models
{
    public class BusinessHoursDto
    {
        public Guid Id { get; set; }
        public Day Day { get; set; }
        public TimeSpan OpeningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }

        public EstablishmentDto Establishment { get; set; }
    }
}
