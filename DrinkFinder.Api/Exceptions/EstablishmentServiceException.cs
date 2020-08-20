using System;

namespace DrinkFinder.Api.Exceptions
{
    public class EstablishmentServiceException : Exception
    {
        public EstablishmentServiceException() { }

        public EstablishmentServiceException(string message) : base(message) { }

        public EstablishmentServiceException(string message, Exception inner) : base(message, inner) { }
    }
}
