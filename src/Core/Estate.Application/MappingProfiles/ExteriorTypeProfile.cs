using AutoMapper;
using Estate.Application.ViewModels.ExteriorType;
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
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductExteriorTypes));
            CreateMap<IncludeExteriorTypeVM, ExteriorType>().ReverseMap();
            CreateMap<ItemExteriorTypeVM, ExteriorType>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductExteriorTypes));
        }
    }
}
