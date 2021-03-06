﻿using DrinkFinder.Common.ValueObjects;
using FluentValidation;

namespace DrinkFinder.Api.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Street)
                .NotEmpty().WithMessage("Cannot be null or empty.")
                .MaximumLength(100).WithMessage("Must be at most {MaxLength} characters long.");

            RuleFor(address => address.BoxNumber)
                .NotEmpty().WithMessage("Cannot be null or empty.")
                .MaximumLength(20).WithMessage("Must be at most {MaxLength} characters long.");

            RuleFor(address => address.PostalCode)
                .NotEmpty().WithMessage("Cannot be null or empty.")
                .MaximumLength(20).WithMessage("Must be at most {MaxLength} characters long.");

            RuleFor(address => address.City)
                .NotEmpty().WithMessage("Cannot be null or empty.")
                .MaximumLength(100).WithMessage("Must be at most {MaxLength} characters long.");

            // TODO: Restrict countries with a list of possible values
            RuleFor(address => address.Country)
                .NotEmpty().WithMessage("Cannot be null or empty.")
                .MaximumLength(20).WithMessage("Must be at most {MaxLength} characters long.");
        }
    }
}
