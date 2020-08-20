using System;

namespace DrinkFinder.Api.Exceptions
{
    public class UserIdMismatchException : Exception
    {
        public UserIdMismatchException() { }

        public UserIdMismatchException(string message) : base(message) { }

        public UserIdMismatchException(string message, Exception innerException) : base(message, innerException) { }
    }
}
