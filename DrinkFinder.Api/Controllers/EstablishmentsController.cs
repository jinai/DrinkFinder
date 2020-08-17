using DrinkFinder.Api.Models;
using DrinkFinder.Api.ResourceParameters;
using DrinkFinder.Api.Services;
using DrinkFinder.Api.Validators;
using DrinkFinder.Infrastructure.ShortCode;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DrinkFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Policy = "Manager")]
    public class EstablishmentsController : ControllerBase
    {
        private readonly IEstablishmentService _establishmentService;

        public EstablishmentsController(IEstablishmentService establishmentService)
        {
            _establishmentService = establishmentService ?? throw new ArgumentNullException(nameof(establishmentService));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EstablishmentDto>>> GetAll([CustomizeValidator(Skip = true)][FromQuery] EstablishmentParameters establishmentParameters,
                                                                              [FromQuery(Name = "Includes")] List<string> includes,
                                                                              [FromServices] EstablishmentParametersValidator validator)
        {
            if (establishmentParameters is null)
            {
                throw new ArgumentNullException(nameof(establishmentParameters));
            }

            if (validator is null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            // Add requested Includes to EstablishmentParameters before validation
            establishmentParameters.Includes = includes;

            // Perform manual validation now that EstablishmentParameters is complete
            var validationResult = validator.Validate(establishmentParameters);
            validationResult.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var establishments = await _establishmentService.GetAll(establishmentParameters);
            return Ok(establishments);
        }

        [HttpGet("{EstablishmentId}")]
        [AllowAnonymous]
        public async Task<ActionResult<EstablishmentDto>> GetById([FromRoute(Name = "EstablishmentId")] Guid establishmentId,
                                                                  [FromQuery(Name = "Includes")] List<string> includes,
                                                                  [FromServices] EstablishmentParametersValidator validator)
        {
            if (validator is null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            // Create an EstablishmentParameters instance containing only the requested Includes
            var parameters = new EstablishmentParameters { Includes = includes };

            // Manual validation
            var validationResult = validator.Validate(parameters);
            validationResult.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var establishment = await _establishmentService.GetById(establishmentId, parameters);

            if (establishment == null)
            {
                return NotFound();
            }

            return Ok(establishment);
        }

        [HttpPost]
        public async Task<ActionResult<EstablishmentDto>> CreateEstablishment([CustomizeValidator(Skip = true)] CreateEstablishmentDto createEstablishment,
                                                                              [FromServices] CreateEstablishmentValidator validator,
                                                                              [FromServices] IShortCodeService shortCodeService)
        {
            if (createEstablishment is null)
            {
                throw new ArgumentNullException(nameof(createEstablishment));
            }

            if (validator is null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            if (shortCodeService is null)
            {
                throw new ArgumentNullException(nameof(shortCodeService));
            }

            // Manual validation because ASP.NET’s validation pipeline is not asynchronous
            var validationResult = await validator.ValidateAsync(createEstablishment);
            validationResult.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            // ShortCode not specified so we try to generate one
            if (createEstablishment.ShortCode == null)
            {
                try
                {
                    createEstablishment.ShortCode = await shortCodeService.NewShortCode();
                }
                catch (TimeoutException ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }
            }

            // Don't forget the current UserId
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var establishmentToReturn = await _establishmentService.Create(createEstablishment, currentUserId);
            return CreatedAtAction(nameof(GetById), new { establishmentId = establishmentToReturn.Id }, establishmentToReturn);
        }

        [HttpDelete("{EstablishmentId}")]
        public async Task<ActionResult> DeleteEstablishment([FromRoute(Name = "EstablishmentId")] Guid establishmentId)
        {
            // Check if the establishment exists
            var establishmentToDelete = await _establishmentService.GetById(establishmentId);

            if (establishmentToDelete == null)
            {
                return NotFound();
            }

            // Check if the establishment is owned by the current user
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (establishmentToDelete.UserId != currentUserId)
            {
                return Forbid();
            }

            try
            {
                await _establishmentService.Delete(establishmentToDelete.Id);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
