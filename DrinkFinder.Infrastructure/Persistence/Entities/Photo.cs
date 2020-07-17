using DrinkFinder.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    [Table("Photo")]
    public class Photo : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public Establishment Establishment { get; set; }
        public Uri Uri { get; set; }

    }
}