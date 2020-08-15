using DrinkFinder.Common.Enums;
using DrinkFinder.Common.ValueObjects;
using System;
using System.Collections.Generic;

namespace DrinkFinder.MvcClient.Models
{
    public class EstablishmentModel
    {
        public Guid Id { get; set; }
        public string ShortCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EstablishmentType Type { get; set; }
        public string VatNumber { get; set; }
        public Uri Logo { get; set; }
        public Uri Banner { get; set; }
        public Uri Website { get; set; }
        public Address Address { get; set; }
        public Socials Socials { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public DateTimeOffset AddedDate { get; set; }

        public ICollection<BusinessHoursModel> BusinessHours { get; set; }
        public ICollection<Uri> Pictures { get; set; }
    }
}
