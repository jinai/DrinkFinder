using DrinkFinder.MvcClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DrinkFinder.MvcClient.Controllers
{
    [Authorize(Policy = "Member")]
    public class EstablishmentsController : Controller
    {
        private readonly IEstablishmentService _establishmentService;

        public EstablishmentsController(IEstablishmentService establishmentService)
        {
            _establishmentService = establishmentService;
        }

        public async Task<IActionResult> Index()
        {
            var establishments = await _establishmentService.GetAll();
            return View(establishments);
        }

        [HttpGet("[controller]/[action]/{establishmentId:guid}")]
        public async Task<IActionResult> Details(Guid establishmentId)
        {
            var establishment = await _establishmentService.GetById(establishmentId);
            return View(establishment);
        }

        [HttpGet("e/{shortCode}")]
        [HttpGet("[controller]/[action]/{shortCode}")]
        public async Task<IActionResult> Details(string shortCode)
        {
            var establishment = await _establishmentService.GetByShortCode(shortCode);

            if (establishment == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(establishment);
        }
    }
}
