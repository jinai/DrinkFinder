using DrinkFinder.Common.Enums;
using System;

namespace DrinkFinder.Common.Extensions
{
    public static class DayOfWeekExtensions
    {
        public static IsoDay ToIsoDay(this DayOfWeek day)
        {
            if (day == DayOfWeek.Sunday)
            {
                return IsoDay.Sunday;
            }

            return (IsoDay)day;
        }
    }
}
