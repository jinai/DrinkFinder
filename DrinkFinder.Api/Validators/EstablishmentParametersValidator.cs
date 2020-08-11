using DrinkFinder.Api.ResourceParameters;
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
            RuleForEach(ep => ep.Includes).Must(include => validIncludes.Contains(include))
                                          .WithMessage($"Supported values are: [{validIncludesString}]. Supplied value: {{PropertyValue}}");
        }
    }
}
