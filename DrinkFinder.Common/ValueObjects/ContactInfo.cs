using System.Collections.Generic;

namespace DrinkFinder.Common.ValueObjects
{
    public class ContactInfo : ValueObject
    {
        public string ProfessionalEmail { get; private set; }
        public string PublicEmail { get; private set; }
        public string PhoneNumber { get; private set; }

        private ContactInfo() { }

        public ContactInfo(string professionalEmail, string publicEmail, string phoneNumber)
        {
            ProfessionalEmail = professionalEmail;
            PublicEmail = publicEmail;
            PhoneNumber = phoneNumber;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ProfessionalEmail;
            yield return PublicEmail;
            yield return PhoneNumber;
        }
    }
}
