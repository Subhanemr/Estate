using AutoMapper;
using Estate.Application.ViewModels;
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
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductFeatures.Select(ma => ma.Product).ToList()));
            CreateMap<IncludeFeaturesVM, Features>().ReverseMap();
            CreateMap<ItemFeaturesVM, Features>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductFeatures.Select(ma => ma.Product).ToList()));
        }
    }
}
