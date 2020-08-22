using DrinkFinder.MvcClient.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DrinkFinder.MvcClient.Controllers
{
    [Authorize(Policy = "Manager")]
    [Route("Manage/{action=Establishments}")]
    public class ManagementController : Controller
    {
        private readonly IEstablishmentService _establishmentService;

        public ManagementController(IEstablishmentService establishmentService)
        {
            _establishmentService = establishmentService;
        }

        public async Task<IActionResult> Establishments()
        {
            var userId = Guid.Parse(User.FindFirst(JwtClaimTypes.Subject).Value);
            var myEstablishments = await _establishmentService.GetAllForUser(userId);
            return View(myEstablishments);
        }
    }
}
