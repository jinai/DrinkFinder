using AutoMapper;
using DrinkFinder.Api.Exceptions;
using DrinkFinder.Api.Models;
using DrinkFinder.Api.ResourceParameters;
using DrinkFinder.Common.Enums;
using DrinkFinder.Infrastructure.Persistence.Entities;
using DrinkFinder.Infrastructure.Persistence.Helpers;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
using DrinkFinder.Infrastructure.ShortCode;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkFinder.Api.Services
{
    public class EstablishmentService : IEstablishmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShortCodeService _shortCodeService;
        private readonly IMapper _mapper;

        public EstablishmentService(IUnitOfWork unitOfWork, IShortCodeService shortCodeService, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _shortCodeService = shortCodeService ?? throw new ArgumentNullException(nameof(shortCodeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<EstablishmentDto>> GetAll(EstablishmentParameters parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var establishments = _unitOfWork.EstablishmentRepo.GetAll();

            if (parameters.Includes != null)
            {
                foreach (var include in parameters.Includes)
                {
                    establishments = establishments.Include(include);
                };
            }

            if (parameters.UserId != Guid.Empty)
            {
                establishments = establishments.Where(e => e.UserId == parameters.UserId);
            }

            if (!string.IsNullOrWhiteSpace(parameters.ShortCode))
            {
                var shortCode = parameters.ShortCode.Trim();
                establishments = establishments.Where(e => e.ShortCode == shortCode);
            }

            if (parameters.Type != null)
            {
                establishments = establishments.Where(e => e.Type == parameters.Type);
            }

            if (parameters.Status != null)
            {
                establishments = establishments.Where(e => e.Status == parameters.Status);
            }

            if (parameters.Day != null)
            {
                establishments = establishments.Include(e => e.BusinessHours).Where(e => e.BusinessHours.Any(bh => bh.Day == parameters.Day && bh.OpeningHour != null));
            }

            establishments = establishments.Paginate(parameters);
            establishments = establishments.OrderBy(e => e.AddedDate);

            var result = await establishments.ToListAsync();

            foreach (var establishment in result)
            {
                establishment.BusinessHours = establishment.BusinessHours?.OrderBy(bh => bh.Day).ToList();
            }

            return _mapper.Map<IEnumerable<EstablishmentDto>>(result);
        }

        public async Task<EstablishmentDto> GetById(Guid establishmentId, EstablishmentParameters parameters = default)
        {
            var establishments = _unitOfWork.EstablishmentRepo.GetById(establishmentId);

            if (parameters?.Includes != null)
            {
                foreach (var include in parameters.Includes)
                {
                    establishments = establishments.Include(include);
                };
            }

            var establishment = await establishments.SingleOrDefaultAsync();

            return _mapper.Map<EstablishmentDto>(establishment);
        }

        public async Task<EstablishmentDto> Create(CreateEstablishmentDto createEstablishmentDto, Guid userId)
        {
            if (createEstablishmentDto is null)
            {
                throw new ArgumentNullException(nameof(createEstablishmentDto));
            }

            if (createEstablishmentDto.ShortCode == null)
            {
                try
                {
                    createEstablishmentDto.ShortCode = await _shortCodeService.NewShortCode();
                }
                catch (ShortCodeServiceException ex)
                {
                    throw new EstablishmentServiceException("Could not generate a short code.", ex);
                }
            }

            var establishmentEntity = _mapper.Map<Establishment>(createEstablishmentDto);
            establishmentEntity.UserId = userId;
            establishmentEntity.Status = EstablishmentStatus.Pending; // Explicitly set it as Pending in case the default changes

            _unitOfWork.EstablishmentRepo.Add(establishmentEntity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<EstablishmentDto>(establishmentEntity);
        }

        public async Task<int> Delete(EstablishmentDto establishmentToDelete, Guid userId)
        {
            if (establishmentToDelete is null)
            {
                throw new ArgumentNullException(nameof(establishmentToDelete));
            }

            // Check if the establishment to delete is owned by the user
            if (establishmentToDelete.UserId != userId)
            {
                throw new UserIdMismatchException("Cannot delete an establishment you don't own.");
            }

            _unitOfWork.EstablishmentRepo.Remove(establishmentToDelete.Id);

            try
            {
                int result = await _unitOfWork.SaveAsync();
                return result;
            }
            catch (DbUpdateException)
            {
                throw new EstablishmentServiceException($"Could not delete establishment (Id: {establishmentToDelete.Id}).");
            }
        }
    }
}
