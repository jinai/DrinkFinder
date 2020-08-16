using DrinkFinder.Common.Enums;
using DrinkFinder.Common.ValueObjects;
using System;

namespace DrinkFinder.Api.Models
{
    public class CreateEstablishmentDto
    {
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
    }
}
