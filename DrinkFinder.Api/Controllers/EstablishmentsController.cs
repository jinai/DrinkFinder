using DrinkFinder.Api.Models;
using DrinkFinder.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var establishments = await _establishmentService.GetAllOrdered();
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
            catch (KeyNotFoundException e)
            {
                return NotFound(e);
            }
        }
    }
}
