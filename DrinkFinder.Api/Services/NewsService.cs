using AutoMapper;
using DrinkFinder.Api.Exceptions;
using DrinkFinder.Api.Models;
using DrinkFinder.Infrastructure.Persistence.Entities;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkFinder.Api.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEstablishmentService _establishmentService;
        private readonly IMapper _mapper;

        public NewsService(IUnitOfWork unitOfWork, IEstablishmentService establishmentService, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _establishmentService = establishmentService ?? throw new ArgumentNullException(nameof(establishmentService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<NewsDto>> GetAll()
        {
            var news = await _unitOfWork.NewsRepo.GetAll().OrderBy(n => n.AddedDate).ToListAsync();
            return _mapper.Map<IEnumerable<NewsDto>>(news);
        }

        public async Task<NewsDto> GetById(Guid newsId)
        {
            var news = await _unitOfWork.NewsRepo.GetById(newsId).SingleOrDefaultAsync();
            return _mapper.Map<NewsDto>(news);
        }

        public async Task<NewsDto> Create(CreateNewsDto createNewsDto, Guid userId)
        {
            if (createNewsDto is null)
            {
                throw new ArgumentNullException(nameof(createNewsDto));
            }

            // Check if the establishment for which the news is going to be published is owned by the user
            var targetEstablishment = await _establishmentService.GetById(createNewsDto.EstablishmentId);

            if (targetEstablishment.UserId != userId)
            {
                throw new UserIdMismatchException("Cannot publish news for an establishment you don't own.");
            }

            var newsEntity = _mapper.Map<News>(createNewsDto);
            newsEntity.UserId = userId;
            _unitOfWork.NewsRepo.Add(newsEntity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<NewsDto>(newsEntity);
        }

        public async Task<int> Delete(NewsDto newsToDelete, Guid userId)
        {
            if (newsToDelete is null)
            {
                throw new ArgumentNullException(nameof(newsToDelete));
            }

            // Check if the news to delete was published by the user
            if (newsToDelete.UserId != userId)
            {
                throw new UserIdMismatchException("Cannot delete news you didn't publish.");
            }

            _unitOfWork.NewsRepo.Remove(newsToDelete.Id);

            try
            {
                var result = await _unitOfWork.SaveAsync();
                return result;
            }
            catch (DbUpdateException)
            {
                throw new NewsServiceException($"Could not delete news (Id: {newsToDelete.Id}).");
            }
        }
    }
}
