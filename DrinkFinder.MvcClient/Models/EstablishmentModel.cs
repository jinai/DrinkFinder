using DrinkFinder.Common.Enums;
using DrinkFinder.Common.Extensions;
using DrinkFinder.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DrinkFinder.MvcClient.Models
{
    public class EstablishmentModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Short code")]
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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTimeOffset AddedDate { get; set; }

        [Display(Name = "Business hours")]
        public ICollection<BusinessHoursModel> BusinessHours { get; set; }
        public ICollection<string> Pictures { get; set; }

        [Display(Name = "Address")]
        public string FormattedAddress => Address.GetFormatted();

        public string Monday => BusinessHours.Single(bhm => bhm.Day == IsoDay.Monday).ToString();
        public string Tuesday => BusinessHours.Single(bhm => bhm.Day == IsoDay.Tuesday).ToString();
        public string Wednesday => BusinessHours.Single(bhm => bhm.Day == IsoDay.Wednesday).ToString();
        public string Thursday => BusinessHours.Single(bhm => bhm.Day == IsoDay.Thursday).ToString();
        public string Friday => BusinessHours.Single(bhm => bhm.Day == IsoDay.Friday).ToString();
        public string Saturday => BusinessHours.Single(bhm => bhm.Day == IsoDay.Saturday).ToString();
        public string Sunday => BusinessHours.Single(bhm => bhm.Day == IsoDay.Sunday).ToString();
    }
}
