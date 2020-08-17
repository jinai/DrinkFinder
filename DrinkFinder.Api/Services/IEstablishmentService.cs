using DrinkFinder.Api.Models;
using DrinkFinder.Api.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkFinder.Api.Services
{
    public interface IEstablishmentService
    {
        Task<IEnumerable<EstablishmentDto>> GetAll(EstablishmentParameters establishmentParameters);
        Task<EstablishmentDto> GetById(Guid establishmentId);
        Task<EstablishmentDto> GetById(Guid establishmentId, EstablishmentParameters establishmentParameters);
        Task<EstablishmentDto> Create(CreateEstablishmentDto createEstablishmentDto, Guid userId);
        Task<int> Delete(Guid establishmentId);
    }
}
