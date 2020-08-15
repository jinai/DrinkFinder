using DrinkFinder.Common.Enums;
using DrinkFinder.Common.ValueObjects;
using System;
using System.Collections.Generic;

namespace DrinkFinder.Api.Models
{
    public class EstablishmentDto
    {
        public Guid Id { get; set; }
        public string ShortCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EstablishmentType Type { get; set; }
        public string VatNumber { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set; }
        public string Website { get; set; }
        public Address Address { get; set; }
        public Socials Socials { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public DateTimeOffset AddedDate { get; set; }

        public ICollection<BusinessHoursDto> BusinessHours { get; set; }
        public ICollection<string> Pictures { get; set; }
    }
}
