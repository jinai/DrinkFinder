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
        Task<EstablishmentDto> GetById(Guid establishmentId, EstablishmentParameters establishmentParameters = default);
        Task<EstablishmentDto> Create(CreateEstablishmentDto createEstablishmentDto, Guid userId);
        Task<int> Delete(EstablishmentDto establishmentToDelete, Guid userId);
    }
}
