﻿using AutoMapper;
using DrinkFinder.Api.Models;
using DrinkFinder.Infrastructure.Persistence.Entities;

namespace DrinkFinder.Api.Mappings
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsDto>()
                .ForMember(
                    dest => dest.PublicationDate,
                    opt => opt.MapFrom(src => src.AddedDate))
                .ForMember(
                    dest => dest.EstablishmentId,
                    opt => opt.MapFrom(src => src.Establishment.Id));
        }
    }
}
