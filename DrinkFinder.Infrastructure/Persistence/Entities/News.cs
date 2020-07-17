using DrinkFinder.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    [Table("News")]
    public class News : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int Publisher { get; set; }
        public Establishment Establishment { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
