using DrinkFinder.Common.Enums;
using DrinkFinder.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    [Table("Establishment")]
    public class Establishment : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public EstablishmentType Type { get; set; }
        public string Name { get; set; }
        public string VATNumber { get; set; }
        public string ProfessionalEmail { get; set; }
        public string Description { get; set; }
        public Uri LogoUri { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PublicEmail { get; set; }
        public Uri Website { get; set; }
        public Socials Socials { get; set; }
        public Timetable Timetable { get; set; }
        public ICollection<Photo> Photos { get; set; }

        public string ShortyUrl { get; set; }


    }
}
