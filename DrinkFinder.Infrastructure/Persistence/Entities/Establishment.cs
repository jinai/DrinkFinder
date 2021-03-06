﻿using DrinkFinder.Common.Enums;
using DrinkFinder.Common.ValueObjects;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
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
        public string VatNumber { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set; }
        public string Website { get; set; }
        public Address Address { get; set; }
        public Socials Socials { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public DateTimeOffset AddedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public Guid UserId { get; set; }
        public ICollection<BusinessHours> BusinessHours { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<News> News { get; set; }
    }
}
