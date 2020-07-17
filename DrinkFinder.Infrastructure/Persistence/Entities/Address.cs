using Microsoft.EntityFrameworkCore;

namespace DrinkFinder.Infrastructure.Persistence.Entities
{
    [Owned]
    public class Address
    {
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string BoxNumber { get; set; }
    }
}
