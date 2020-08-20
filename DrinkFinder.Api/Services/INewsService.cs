using DrinkFinder.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkFinder.Api.Services
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> GetAll();
        Task<NewsDto> GetById(Guid newsId);
        Task<NewsDto> Create(CreateNewsDto createNewsDto, Guid userId);
        Task<int> Delete(NewsDto newsToDelete, Guid userId);
    }
}
