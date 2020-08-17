using System;

namespace DrinkFinder.Api.Models
{
    public class CreateNewsDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Banner { get; set; }

        public Guid EstablishmentId { get; set; }
    }
}
