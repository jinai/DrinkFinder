using DrinkFinder.Common.Enums;
using System;
using System.Collections.Generic;

namespace DrinkFinder.Api.ResourceParameters
{
    public class EstablishmentParameters : BaseParameters
    {
        public Guid UserId { get; set; }
        public string ShortCode { get; set; }
        public EstablishmentStatus? Status { get; set; }
        public IsoDay? Day { get; set; }
        internal List<string> Includes { get; set; }
    }
}
