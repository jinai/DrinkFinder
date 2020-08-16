using DrinkFinder.Api.Models;
using DrinkFinder.Infrastructure.ShortCode;
using DrinkFinder.Infrastructure.Vat;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DrinkFinder.Api.Validators
{
    public class CreateEstablishmentValidator : AbstractValidator<CreateEstablishmentDto>
    {
        private readonly IVatService _vatService;
        private readonly IShortCodeService _shortCodeService;

        public CreateEstablishmentValidator(IShortCodeService shortCodeService, IVatService vatService)
        {
            _shortCodeService = shortCodeService;
            _vatService = vatService;

            When(createEstab => createEstab.ShortCode != null, () =>
            {
                RuleFor(createEstab => createEstab.ShortCode)
                    .MinimumLength(ShortCodeService.MinSize).WithMessage("Must be longer than or equal to {MinLength} characters.")
                    .MaximumLength(ShortCodeService.MaxSize).WithMessage("Must be shorter than or equal to {MaxLength} characters.")
                    .Must(ShortCodeService.IsValid).WithMessage($"Invalid value : '{{PropertyValue}}'. Allowed characters: {ShortCodeService.AllowedCharacters}")

                    .DependentRules(() =>
                    {
                        RuleFor(createEstab => createEstab.ShortCode)
                            .MustAsync(IsAvailableShortCode).WithMessage("{PropertyValue} is already taken, please choose something else.");
                    });
            });

            RuleFor(createEstab => createEstab.Name)
                .NotEmpty().WithMessage("Cannot be null or empty.")
                .MaximumLength(50).WithMessage("Must be shorter than or equal to {MaxLength} characters.");

            RuleFor(createEstab => createEstab.Description)
                .NotEmpty().WithMessage("Cannot be null or empty.")
                .MaximumLength(2000).WithMessage("Must be shorter than or equal to {MaxLength} characters.");

            RuleFor(createEstab => createEstab.VatNumber)
                .NotEmpty().WithMessage("Cannot be null or empty.")

                .DependentRules(() =>
                {
                    RuleFor(createEstab => createEstab.VatNumber)
                        .MustAsync(IsValidVatNumber).WithMessage("Invalid VAT number or invalid format: '{PropertyValue}'.");
                });

            RuleFor(createEstab => createEstab.Logo)
                .Must(logo => Uri.TryCreate(logo, UriKind.Absolute, out _)).WithMessage("Invalid URI: '{PropertyValue}'.")
                    .When(createEstab => createEstab.Logo != null);

            RuleFor(createEstab => createEstab.Banner)
                .Must(banner => Uri.TryCreate(banner, UriKind.Absolute, out _)).WithMessage("Invalid URI: '{PropertyValue}'.")
                    .When(createEstab => createEstab.Banner != null);

            RuleFor(createEstab => createEstab.Website)
                .Must(website => Uri.TryCreate(website, UriKind.Absolute, out _)).WithMessage("Invalid URI: '{PropertyValue}'.")
                    .When(createEstab => createEstab.Website != null);

            RuleFor(createEstab => createEstab.Address)
                .NotNull().WithMessage("Cannot be null.")
                .SetValidator(new AddressValidator());

            RuleFor(createEstab => createEstab.Socials)
                .SetValidator(new SocialsValidator());

            RuleFor(createEstab => createEstab.ContactInfo)
                .NotNull().WithMessage("Cannot be null.")
                .SetValidator(new ContactInfoValidator());
        }

        public async Task<bool> IsAvailableShortCode(string shortCode, CancellationToken token)
        {
            return await _shortCodeService.IsAvailable(shortCode);
        }

        public async Task<bool> IsValidVatNumber(string vatNumber, CancellationToken token)
        {
            var vatResponse = await _vatService.Validate(vatNumber);
            return vatResponse.IsValid && vatResponse.IsFormatValid;
        }
    }
}
