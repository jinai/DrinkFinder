using DrinkFinder.Common.Enums;
using DrinkFinder.MvcClient.Models;
using DrinkFinder.MvcClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkFinder.MvcClient.Controllers
{
    [Authorize(Policy = "Member")]
    [ApiController]
    [Route("[controller]")]
    public class ApiProxyController : ControllerBase
    {
        private readonly IEstablishmentService _establishmentService;
        private readonly IGeocodingService _geocodingService;

        public ApiProxyController(IEstablishmentService establishmentService, IGeocodingService geocodingService)
        {
            _establishmentService = establishmentService;
            _geocodingService = geocodingService;
        }

        [AllowAnonymous]
        [HttpGet("GetMarkers")]
        public async Task<ActionResult<IEnumerable<MapMarker>>> GetMarkers(IsoDay? day)
        {
            IEnumerable<EstablishmentModel> establishments;

            if (!User.Identity.IsAuthenticated)
            {
                if (day != null)
                {
                    return Unauthorized();
                }

                establishments = await _establishmentService.GetAll();
            }
            else
            {
                establishments = await _establishmentService.GetAllOpenOn((IsoDay)day);
            }

            var markers = await _geocodingService.GetMarkers(establishments);
            return Ok(markers);
        }
    }
}
