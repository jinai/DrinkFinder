using DrinkFinder.Api.Models;
using DrinkFinder.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<NewsDto>> CreateNews(CreateNewsDto createNews)
        {
            if (createNews is null)
            {
                throw new ArgumentNullException(nameof(createNews));
            }

            // Check if the establishment is owned by the current user
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var targetEstablishment = await _establishmentService.GetById(createNews.EstablishmentId);

            if (targetEstablishment.UserId != currentUserId)
            {
                return Forbid();
            }

            var newsToReturn = await _newsService.Create(createNews, currentUserId);
            return CreatedAtAction(nameof(GetById), new { newsId = newsToReturn.Id }, newsToReturn);
        }

        [HttpDelete("{NewsId}")]
        public async Task<ActionResult> DeleteNews([FromRoute(Name = "NewsId")] Guid newsId)
        {
            // Check if the news exists
            var newsToDelete = await _newsService.GetById(newsId);

            if (newsToDelete == null)
            {
                return NotFound();
            }

            // Check if the news was published by the current user
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (newsToDelete.UserId != currentUserId)
            {
                return Forbid();
            }

            try
            {
                await _newsService.Delete(newsToDelete.Id);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
