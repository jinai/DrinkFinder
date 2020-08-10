using AutoMapper;
using DrinkFinder.Api.Models;
using DrinkFinder.Api.ResourceParameters;
using DrinkFinder.Common.Enums;
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

        public async Task<IEnumerable<EstablishmentDto>> GetAllApproved()
        {
            var establishments = await _unitOfWork.EstablishmentRepo.GetWhere(e => e.Status == EstablishmentStatus.Approved).Include(e => e.BusinessHours).OrderBy(e => e.AddedDate).ToListAsync();
            foreach (var establishment in establishments)
            {
                establishment.BusinessHours = establishment.BusinessHours.OrderBy(bh => bh.Day).ToList();
            }
            return _mapper.Map<IEnumerable<EstablishmentDto>>(establishments);
        }

        public async Task<EstablishmentDto> GetById(Guid establishmentId)
        {
            var establishment = await _unitOfWork.EstablishmentRepo.GetById(establishmentId);

            if (establishment == null)
            {
                throw new Exception($"The establishment with ID = {establishmentId} doesn't exist."); // TODO: Return a ServiceResult or a custom exception
            }
            return _mapper.Map<EstablishmentDto>(establishment);
        }
    }
}
