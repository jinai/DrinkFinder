using DrinkFinder.Api.Exceptions;
using DrinkFinder.Api.Models;
using DrinkFinder.Api.ResourceParameters;
using DrinkFinder.Api.Services;
using DrinkFinder.Api.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<EstablishmentDto>> CreateEstablishment([CustomizeValidator(Skip = true)] CreateEstablishmentDto createEstablishmentDto,
                                                                              [FromServices] CreateEstablishmentValidator validator)
        {
            if (createEstablishmentDto is null)
            {
                throw new ArgumentNullException(nameof(createEstablishmentDto));
            }

            if (validator is null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            // Manual validation because ASP.NET’s validation pipeline is not asynchronous
            var validationResult = await validator.ValidateAsync(createEstablishmentDto);
            validationResult.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            try
            {
                var establishmentToReturn = await _establishmentService.Create(createEstablishmentDto, currentUserId);
                return CreatedAtAction(nameof(GetById), new { establishmentId = establishmentToReturn.Id }, establishmentToReturn);
            }
            catch (EstablishmentServiceException ex)
            {
                return new JsonResult(new { ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpDelete("{EstablishmentId}")]
        public async Task<ActionResult> DeleteEstablishment([FromRoute(Name = "EstablishmentId")] Guid establishmentId)
        {
            var establishmentToDelete = await _establishmentService.GetById(establishmentId);

            if (establishmentToDelete == null)
            {
                return NotFound();
            }

            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            try
            {
                await _establishmentService.Delete(establishmentToDelete, currentUserId);
                return NoContent();
            }
            catch (UserIdMismatchException ex)
            {
                return new JsonResult(new { ex.Message }) { StatusCode = StatusCodes.Status403Forbidden };
            }
            catch (EstablishmentServiceException ex)
            {
                return new JsonResult(new { ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
