using AutoMapper;
using DrinkFinder.Api.Models;
using DrinkFinder.Infrastructure.Persistence.Entities;
using System.Linq;

namespace DrinkFinder.Api.Mappings
{
    public class EstablishmentProfile : Profile
    {
        public EstablishmentProfile()
        {
            CreateMap<Establishment, EstablishmentDto>()
                .ForMember(
                    dest => dest.Pictures,
                    opt => opt.MapFrom(src => src.Pictures.Select(p => p.Location)));

            CreateMap<CreateEstablishmentDto, Establishment>();
        }
    }
}
