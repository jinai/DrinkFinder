﻿using DrinkFinder.Api.ResourceParameters;
using FluentValidation;
using System.Collections.Generic;

namespace DrinkFinder.Api.Validators
{
    public class EstablishmentParametersValidator : AbstractValidator<EstablishmentParameters>
    {
        private static readonly HashSet<string> validIncludes = new HashSet<string> { "BusinessHours", "Pictures" };
        private static readonly string validIncludesString = string.Join(", ", validIncludes);

        public EstablishmentParametersValidator()
        {
            Include(new BaseParametersValidator());

            RuleForEach(estabParam => estabParam.Includes)
                .Must(include => validIncludes.Contains(include)).WithMessage($"Invalid value: '{{PropertyValue}}'. Supported values: {validIncludesString}.");
        }
    }
}
