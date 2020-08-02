using DrinkFinder.Common.Enums;
using DrinkFinder.Common.Interfaces;
using DrinkFinder.Common.ValueObjects;
using DrinkFinder.Infrastructure.Persistence.Identity;
using System;
using System.Collections.Generic;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    public class Establishment : IEntity<Guid>
    {
        public Guid Id { get; set; }
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
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public ApplicationUser Manager { get; set; }
        public ICollection<BusinessHours> BusinessHours { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<News> News { get; set; }
    }
}
