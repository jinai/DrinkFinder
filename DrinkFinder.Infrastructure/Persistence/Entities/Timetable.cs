using DrinkFinder.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    [Table("Timetable")]
    public class Timetable : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public Establishment Establishment { get; set; }
        public TimeSpan OpeningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }
    }
}