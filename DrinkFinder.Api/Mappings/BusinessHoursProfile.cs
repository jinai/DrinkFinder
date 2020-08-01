using AutoMapper;
using DrinkFinder.Api.Models;
using DrinkFinder.Infrastructure.Persistence.Entities;

namespace DrinkFinder.Api.Mappings
{
    public class BusinessHoursProfile : Profile
    {
        public BusinessHoursProfile()
        {
            CreateMap<BusinessHours, BusinessHoursDto>();
        }
    }
}
