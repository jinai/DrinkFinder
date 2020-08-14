using DrinkFinder.Api.Models;
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
    [Authorize("Member")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
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
    }
}
