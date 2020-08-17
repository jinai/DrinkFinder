using DrinkFinder.Common.ValueObjects;
using FluentValidation;

namespace DrinkFinder.Api.Validators
{
    public class ContactInfoValidator : AbstractValidator<ContactInfo>
    {
        public ContactInfoValidator()
        {
            RuleFor(contactInfo => contactInfo.ProfessionalEmail)
                .NotEmpty().WithMessage("Cannot be null or empty.")
                .EmailAddress().WithMessage("Invalid email address: '{PropertyValue}'.");

            RuleFor(contactInfo => contactInfo.PublicEmail)
                .EmailAddress().WithMessage("Invalid email address: '{PropertyValue}'.")
                    .When(contactInfo => contactInfo.PublicEmail != null);

            RuleFor(contactInfo => contactInfo.PhoneNumber)
                .MaximumLength(25).WithMessage("Must be at most {MaxLength} characters long.");
            // TODO: Validate contactInfo.PhoneNumber using an external API
        }
    }
}
