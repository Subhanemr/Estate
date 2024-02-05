using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class ExteriorTypeProfile : Profile
    {
        public ExteriorTypeProfile()
        {
            CreateMap<CreateExteriorTypeVM, ExteriorType>().ReverseMap();
            CreateMap<UpdateExteriorTypeVM, ExteriorType>().ReverseMap();
            CreateMap<GetExteriorTypeVM, ExteriorType>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductExteriorTypes.Select(ma => ma.Product).ToList()));
            CreateMap<IncludeExteriorTypeVM, ExteriorType>().ReverseMap();
            CreateMap<ItemExteriorTypeVM, ExteriorType>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductExteriorTypes.Select(ma => ma.Product).ToList()));
        }
    }
}
