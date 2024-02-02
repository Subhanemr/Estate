using AutoMapper;
using Estate.Application.ViewModels.RoofType;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class RoofTypeProfile : Profile
    {
        public RoofTypeProfile()
        {
            CreateMap<CreateRoofTypeVM, RoofType>().ReverseMap();
            CreateMap<UpdateRoofTypeVM, RoofType>().ReverseMap();
            CreateMap<GetRoofTypeVM, RoofType>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductRoofTypes));
            CreateMap<IncludeRoofTypeVM, RoofType>().ReverseMap();
            CreateMap<ItemRoofTypeVM, RoofType>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductRoofTypes));
        }
    }
}
