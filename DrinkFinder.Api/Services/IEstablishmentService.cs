using DrinkFinder.Api.Models;
using DrinkFinder.Api.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkFinder.Api.Services
{
    public interface IEstablishmentService
    {
        Task<IEnumerable<EstablishmentDto>> GetAllApproved();
        Task<EstablishmentDto> GetById(Guid establishmentId);
    }
}
