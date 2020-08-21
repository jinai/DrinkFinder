using DrinkFinder.Common.Enums;
using DrinkFinder.Common.Extensions;
using DrinkFinder.MvcClient.Models;
using DrinkFinder.MvcClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    day ??= DateTime.Now.DayOfWeek.ToIsoDay();
                    establishments = await _establishmentService.GetAllOpenOn((IsoDay)day);
                }
                else
                {
                    if (day != null)
                    {
                        return Unauthorized();
                    }

                    establishments = await _establishmentService.GetAll();
                }
            }
            catch (Exception) // TODO: Better exception handling
            {
                establishments = new List<EstablishmentModel>();
            }

            var markers = await _geocodingService.GetMarkers(establishments);
            return Ok(markers);
        }
    }
}
