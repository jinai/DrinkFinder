using System;

namespace DrinkFinder.Api.Exceptions
{
    public class NewsServiceException : Exception
    {
        public NewsServiceException() { }

        public NewsServiceException(string message) : base(message) { }

        public NewsServiceException(string message, Exception inner) : base(message, inner) { }
    }
}
