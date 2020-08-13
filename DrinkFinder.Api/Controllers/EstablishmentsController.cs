using DrinkFinder.Api.Models;
using DrinkFinder.Api.ResourceParameters;
using DrinkFinder.Api.Services;
using DrinkFinder.Api.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Policy = "Member")]
    public class EstablishmentsController : ControllerBase
    {
        private readonly IEstablishmentService _establishmentService;

        public EstablishmentsController(IEstablishmentService establishmentService)
        {
            _establishmentService = establishmentService ?? throw new ArgumentNullException(nameof(establishmentService));
        }

        [HttpGet()]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EstablishmentDto>>> GetAll([CustomizeValidator(Skip = true)][FromQuery] EstablishmentParameters establishmentParameters,
                                                                              [FromQuery(Name = "Includes")] List<string> includes)
        {
            if (establishmentParameters is null)
            {
                throw new ArgumentNullException(nameof(establishmentParameters));
            }

            // Add requested Includes to EstablishmentParameters before validation
            establishmentParameters.Includes = includes;

            // Perform manual validation now that EstablishmentParameters is complete
            var validator = new EstablishmentParametersValidator();
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
        public async Task<ActionResult<EstablishmentDto>> GetById([FromRoute(Name = "EstablishmentId")] Guid establishmentId, [FromQuery(Name = "Includes")] List<string> includes)
        {
            // Create an EstablishmentParameters instance containing only the requested Includes
            var parameters = new EstablishmentParameters { Includes = includes };

            // Manual validation
            var validator = new EstablishmentParametersValidator();
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
    }
}
