using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrinkFinder.Common.ValueObjects
{
    public class ContactInfo : ValueObject
    {
        [Display(Name = "Professional email")]
        public string ProfessionalEmail { get; private set; }
        [Display(Name = "Public email")]
        public string PublicEmail { get; private set; }
        [Display(Name = "Phone number")]
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
