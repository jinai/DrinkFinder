using System;

namespace DrinkFinder.Api.Models
{
    public class NewsDto
    {
        public Guid Id { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Uri Banner { get; set; }

        public EstablishmentDto Establishment { get; set; }
    }
}
