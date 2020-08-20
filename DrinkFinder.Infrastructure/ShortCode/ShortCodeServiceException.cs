using System;

namespace DrinkFinder.Infrastructure.ShortCode
{
    public class ShortCodeServiceException : Exception
    {
        public ShortCodeServiceException() { }

        public ShortCodeServiceException(string message) : base(message) { }

        public ShortCodeServiceException(string message, Exception inner) : base(message, inner) { }
    }
}
