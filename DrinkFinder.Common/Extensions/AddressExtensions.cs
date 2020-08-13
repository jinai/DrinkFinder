using DrinkFinder.Common.ValueObjects;

namespace DrinkFinder.Common.Extensions
{
    public static class AddressExtensions
    {
        public static string GetFormatted(this Address a)
        {
            var formatted = $"{a.Street} {a.BoxNumber}, {a.PostalCode} {a.City} {a.Country}";
            return formatted;
        }
    }
}
