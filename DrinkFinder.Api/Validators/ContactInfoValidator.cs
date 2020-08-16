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
                .MaximumLength(25).WithMessage("Must be shorter than or equal to {MaxLength} characters.");
            // TODO: Validate contactInfo.PhoneNumber using an external API
        }
    }
}
