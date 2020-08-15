using DrinkFinder.Common.Enums;
using System;
using System.Collections.Generic;

namespace DrinkFinder.MvcClient.Models
{
    public class MapMarker
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ShortCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EstablishmentType Type { get; set; }
        public string Logo { get; set; }
        public string FormattedAddress { get; set; }
        public IEnumerable<BusinessHoursModel> BusinessHours { get; set; }
    }
}
