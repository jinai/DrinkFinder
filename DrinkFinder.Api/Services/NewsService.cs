using AutoMapper;
using DrinkFinder.Api.Models;
using DrinkFinder.Infrastructure.Persistence.Entities;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkFinder.Api.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<NewsDto>> GetAll()
        {
            var news = await _unitOfWork.NewsRepo.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<NewsDto>>(news);
        }

        public async Task<NewsDto> GetById(Guid newsId)
        {
            var news = await _unitOfWork.NewsRepo.GetById(newsId).SingleOrDefaultAsync();
            return _mapper.Map<NewsDto>(news);
        }

        public async Task<NewsDto> Create(CreateNewsDto createNewsDto, Guid userId)
        {
            var newsEntity = _mapper.Map<News>(createNewsDto);
            newsEntity.UserId = userId;
            _unitOfWork.NewsRepo.Add(newsEntity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<NewsDto>(newsEntity);
        }

        public Task<int> Delete(Guid newsId)
        {
            _unitOfWork.NewsRepo.Remove(newsId);
            return _unitOfWork.SaveAsync();
        }
    }
}
