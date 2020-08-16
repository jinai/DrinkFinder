using DrinkFinder.Common.ValueObjects;
using System;

namespace DrinkFinder.Common.Extensions
{
    public static class AddressExtensions
    {
        public static string GetFormatted(this Address address)
        {
            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            var formatted = $"{address.Street} {address.BoxNumber}, {address.PostalCode} {address.City} {address.Country}";
            return formatted;
        }
    }
}
