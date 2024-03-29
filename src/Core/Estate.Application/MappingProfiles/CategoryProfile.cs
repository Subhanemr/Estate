﻿using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryVM, Category>().ReverseMap();
            CreateMap<Category, UpdateCategoryVM>().ReverseMap();
            CreateMap<GetCategoryVM, Category>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.Products.ToList()));
            CreateMap<IncludeCategoryVM, Category>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.Products.ToList()));
            CreateMap<ItemCategoryVM, Category>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.Products.ToList()));
        }
    }
}
