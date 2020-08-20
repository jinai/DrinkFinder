using DrinkFinder.Api.Exceptions;
using DrinkFinder.Api.Models;
using DrinkFinder.Api.Services;
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
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IEstablishmentService _establishmentService;

        public NewsController(INewsService newsService, IEstablishmentService establishmentService)
        {
            _newsService = newsService;
            _establishmentService = establishmentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<NewsDto>>> GetAll()
        {
            var news = await _newsService.GetAll();
            return Ok(news);
        }

        [HttpGet("{NewsId}")]
        [AllowAnonymous]
        public async Task<ActionResult<NewsDto>> GetById([FromRoute(Name = "NewsId")] Guid newsId)
        {
            var news = await _newsService.GetById(newsId);

            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }

        [HttpPost]
        public async Task<ActionResult<NewsDto>> CreateNews(CreateNewsDto createNewsDto)
        {
            if (createNewsDto is null)
            {
                throw new ArgumentNullException(nameof(createNewsDto));
            }

            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            try
            {
                var newsToReturn = await _newsService.Create(createNewsDto, currentUserId);
                return CreatedAtAction(nameof(GetById), new { newsId = newsToReturn.Id }, newsToReturn);
            }
            catch (UserIdMismatchException ex)
            {
                return new JsonResult(new { ex.Message }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }

        [HttpDelete("{NewsId}")]
        public async Task<ActionResult> DeleteNews([FromRoute(Name = "NewsId")] Guid newsId)
        {
            var newsToDelete = await _newsService.GetById(newsId);

            if (newsToDelete == null)
            {
                return NotFound();
            }

            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            try
            {
                await _newsService.Delete(newsToDelete, currentUserId);
                return NoContent();
            }
            catch (UserIdMismatchException ex)
            {
                return new JsonResult(new { ex.Message }) { StatusCode = StatusCodes.Status403Forbidden };
            }
            catch (NewsServiceException ex)
            {
                return new JsonResult(new { ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
