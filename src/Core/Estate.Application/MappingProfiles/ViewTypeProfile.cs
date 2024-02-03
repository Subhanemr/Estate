using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class ViewTypeProfile : Profile
    {
        public ViewTypeProfile()
        {
            CreateMap<CreateViewTypeVM, ViewType>().ReverseMap();
            CreateMap<UpdateViewTypeVM, ViewType>().ReverseMap();
            CreateMap<GetViewTypeVM, ViewType>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductViewTypes));
            CreateMap<IncludeViewTypeVM, ViewType>().ReverseMap();
            CreateMap<ItemViewTypeVM, ViewType>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductViewTypes));
        }
    }
}
