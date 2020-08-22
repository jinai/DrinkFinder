using DrinkFinder.Common.Enums;
using DrinkFinder.Common.Extensions;
using DrinkFinder.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrinkFinder.MvcClient.Models
{
    public class EstablishmentModel
    {
        public Guid Id { get; set; }
        public string ShortCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EstablishmentType Type { get; set; }
        [Display(Name = "VAT Number")]
        public string VatNumber { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set; }
        public string Website { get; set; }
        public Address Address { get; set; }
        public Socials Socials { get; set; }
        public ContactInfo ContactInfo { get; set; }
        [Display(Name = "Registered since")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTimeOffset AddedDate { get; set; }

        public ICollection<BusinessHoursModel> BusinessHours { get; set; }
        public ICollection<string> Pictures { get; set; }

        [Display(Name = "Address")]
        public string FormattedAddress => Address.GetFormatted();
    }
}
