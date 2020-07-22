using DrinkFinder.Common.Enums;
using DrinkFinder.Infrastructure.Persistence.Abstract;
using DrinkFinder.Infrastructure.Persistence.ValueObjects;
using System;
using System.Collections.Generic;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    public class Establishment : BaseEntity
    {
        public string ShortCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EstablishmentType Type { get; set; }
        public EstablishmentStatus Status { get; set; }
        public string VATNumber { get; set; }
        public Uri Logo { get; set; }
        public Uri Banner { get; set; }
        public Uri Website { get; set; }
        public Address Address { get; set; }
        public Socials Socials { get; set; }
        public ContactInfo ContactInfo { get; set; }

        public Timetable Timetable { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<News> News { get; set; }
    }
}
