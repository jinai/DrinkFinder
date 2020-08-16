using AutoMapper;
using DrinkFinder.Api.Models;
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
    }
}
