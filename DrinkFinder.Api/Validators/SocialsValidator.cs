using DrinkFinder.Common.ValueObjects;
using FluentValidation;
using System;

namespace DrinkFinder.Api.Validators
{
    public class SocialsValidator : AbstractValidator<Socials>
    {
        public SocialsValidator()
        {
            RuleFor(socials => socials.Instagram)
                .Must(instagram => Uri.TryCreate(instagram, UriKind.Absolute, out _)).WithMessage("Invalid URI: '{PropertyValue}'.")
                    .When(socials => socials.Instagram != null);

            RuleFor(socials => socials.Facebook)
                .Must(facebook => Uri.TryCreate(facebook, UriKind.Absolute, out _)).WithMessage("Invalid URI: '{PropertyValue}'.")
                    .When(socials => socials.Facebook != null);

            RuleFor(socials => socials.Twitter)
                .Must(twitter => Uri.TryCreate(twitter, UriKind.Absolute, out _)).WithMessage("Invalid URI: '{PropertyValue}'.")
                    .When(socials => socials.Twitter != null);

            RuleFor(socials => socials.LinkedIn)
                .Must(linkedIn => Uri.TryCreate(linkedIn, UriKind.Absolute, out _)).WithMessage("Invalid URI: '{PropertyValue}'.")
                    .When(socials => socials.LinkedIn != null);
        }
    }
}