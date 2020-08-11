using DrinkFinder.Api.ResourceParameters;
using FluentValidation;

namespace DrinkFinder.Api.Validators
{
    public class BaseParametersValidator : AbstractValidator<BaseParameters>
    {
        public BaseParametersValidator()
        {
            RuleFor(baseParam => baseParam.PageIndex)
                .GreaterThanOrEqualTo(BaseParameters.minPageIndex).WithMessage("Must be greater than or equal to {ComparisonValue}");
            RuleFor(baseParam => baseParam.PageSize)
                .GreaterThanOrEqualTo(BaseParameters.minPageSize).WithMessage("Must be greater than or equal to {ComparisonValue}")
                .LessThanOrEqualTo(BaseParameters.maxPageSize).WithMessage("Must be less than or equal to {ComparisonValue}");
        }
    }
}
