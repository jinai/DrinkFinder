using DrinkFinder.MvcClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var establishments = await _establishmentService.GetAll();
            return View(establishments);
        }
    }
}
