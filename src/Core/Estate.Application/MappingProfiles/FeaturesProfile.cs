﻿using AutoMapper;
using Estate.Application.ViewModels.Features;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class FeaturesProfile : Profile
    {
        public FeaturesProfile()
        {
            CreateMap<CreateFeaturesVM, Features>().ReverseMap();
            CreateMap<UpdateFeaturesVM, Features>().ReverseMap();
            CreateMap<GetFeaturesVM, Features>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductFeatures));
            CreateMap<IncludeFeaturesVM, Features>().ReverseMap();
            CreateMap<ItemFeaturesVM, Features>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductFeatures));
        }
    }
}
