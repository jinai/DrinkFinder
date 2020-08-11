using AutoMapper;
using DrinkFinder.Api.Helpers;
using DrinkFinder.Api.Models;
using DrinkFinder.Api.ResourceParameters;
using DrinkFinder.Infrastructure.Persistence.Interfaces;
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
        private readonly IMapper _mapper;

        public EstablishmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
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

        public async Task<EstablishmentDto> GetById(Guid establishmentId, EstablishmentParameters parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var establishments = _unitOfWork.EstablishmentRepo.GetById(establishmentId);

            if (parameters.Includes != null)
            {
                foreach (var include in parameters.Includes)
                {
                    establishments = establishments.Include(include);
                };
            }

            var establishment = await establishments.FirstOrDefaultAsync();

            return _mapper.Map<EstablishmentDto>(establishment);
        }
    }
}
