using DrinkFinder.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkFinder.Api.Services
{
    public interface IEstablishmentService
    {
        Task<IEnumerable<EstablishmentDto>> GetAllOrdered();
        Task<EstablishmentDto> GetById(Guid establishmentId);
    }
}
