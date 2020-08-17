using DrinkFinder.Api.Models;
using FluentValidation;
using System;

namespace DrinkFinder.Api.Validators
{
    public class CreateNewsValidator : AbstractValidator<CreateNewsDto>
    {
        public CreateNewsValidator()
        {
            RuleFor(createNews => createNews.Title)
                .NotEmpty().WithMessage("Cannot be null or empty.")
                .MaximumLength(255).WithMessage("Must be at most {MaxLength} characters long.");

            RuleFor(createNews => createNews.Content)
                .NotEmpty().WithMessage("Cannot be null or empty.")
                .MaximumLength(20000).WithMessage("Must be at most {MaxLength} characters long.");

            RuleFor(createNews => createNews.Banner)
                .Must(banner => Uri.TryCreate(banner, UriKind.Absolute, out _)).WithMessage("Invalid URI: '{PropertyValue}'.")
                    .When(createNews => createNews.Banner != null);

            RuleFor(createNews => createNews.EstablishmentId)
                .NotEmpty().WithMessage("Cannot be null or empty.");
        }
    }
}
