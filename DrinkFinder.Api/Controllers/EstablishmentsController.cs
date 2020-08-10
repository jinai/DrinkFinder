using DrinkFinder.Api.Models;
using DrinkFinder.Api.ResourceParameters;
using DrinkFinder.Api.Services;
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
    [Authorize(Policy = "Admin")]
    public class EstablishmentsController : ControllerBase
    {
        private readonly IEstablishmentService _establishmentService;

        public EstablishmentsController(IEstablishmentService establishmentService)
        {
            _establishmentService = establishmentService ?? throw new ArgumentNullException(nameof(establishmentService));
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<EstablishmentDto>>> GetAll()
        {
            var establishments = await _establishmentService.GetAllApproved();
            return Ok(establishments);
        }

        [HttpGet("{establishmentId}")]
        public async Task<ActionResult<EstablishmentDto>> GetById(Guid establishmentId)
        {
            try
            {
                var establishment = await _establishmentService.GetById(establishmentId);
                return Ok(establishment);
            }
            catch (Exception exc)
            {
                return NotFound(exc);
            }
        }
    }
}
