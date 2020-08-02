using System;

namespace DrinkFinder.Api.Models
{
    public class PictureDto
    {
        public Guid Id { get; set; }
        public Uri Location { get; set; }
        public DateTimeOffset AddedDate { get; set; }

        public EstablishmentDto Establishment { get; set; }
    }
}
