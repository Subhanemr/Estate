using AutoMapper;
using Estate.Application.ViewModels.Category;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryVM, Category>().ReverseMap();
            CreateMap<UpdateCategoryVM, Category>().ReverseMap();
            CreateMap<GetCategoryVM, Category>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<IncludeCategoryVM, Category>().ReverseMap();
            CreateMap<ItemCategoryVM, Category>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.Products));
        }
    }
}
