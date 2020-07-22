using DrinkFinder.Infrastructure.Persistence.Abstract;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    public class Photo : BaseEntity
    {
        public Uri Location { get; set; }

        public Establishment Establishment { get; set; }
    }
}