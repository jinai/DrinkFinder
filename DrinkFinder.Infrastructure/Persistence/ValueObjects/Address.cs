using DrinkFinder.Common.Abstract;
using System.Collections.Generic;

namespace DrinkFinder.Infrastructure.Persistence.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string BoxNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        private Address() { }

        public Address(string street, string boxNumber, string postalCode, string city, string country)
        {
            Street = street;
            BoxNumber = boxNumber;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return BoxNumber;
            yield return PostalCode;
            yield return City;
            yield return Country;
        }
    }
}
