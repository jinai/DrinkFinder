using DrinkFinder.Infrastructure.Persistence.Abstract;
using System;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    public class News : BaseEntity
    {
        public DateTime PublicationDate { get; set; }
        public Uri Banner { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Establishment Establishment { get; set; }
    }
}
