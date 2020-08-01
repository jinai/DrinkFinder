using AutoMapper;
using DrinkFinder.Api.Models;
using DrinkFinder.Infrastructure.Persistence.Entities;

namespace DrinkFinder.Api.Mappings
{
    public class EstablishmentProfile : Profile
    {
        public EstablishmentProfile()
        {
            CreateMap<Establishment, EstablishmentDto>();
        }
    }
}
