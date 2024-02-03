using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductVM, Product>().ReverseMap();
            CreateMap<UpdateProductVM, Product>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.ProductImages.ToList()));
            CreateMap<GetProductVM, Product>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.ProductImages.ToList()))
                .ForMember(x => x.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(x => x.Comments, opt => opt.MapFrom(src => src.ProductComments.ToList()))
                .ForMember(x => x.Features, opt => opt.MapFrom(src => src.ProductFeatures.Select(ma => ma.Features).ToList()))
                .ForMember(x => x.ExteriorTypes, opt => opt.MapFrom(src => src.ProductExteriorTypes.Select(ma => ma.ExteriorType).ToList()))
                .ForMember(x => x.ParkingTypes, opt => opt.MapFrom(src => src.ProductParkingTypes.Select(ma => ma.ParkingType).ToList()))
                .ForMember(x => x.RoofTypes, opt => opt.MapFrom(src => src.ProductRoofTypes.Select(ma => ma.RoofType).ToList()))
                .ForMember(x => x.ViewTypes, opt => opt.MapFrom(src => src.ProductViewTypes.Select(ma => ma.ViewType).ToList()));
            CreateMap<IncludeProductVM, Product>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.ProductImages.ToList()));
            CreateMap<ItemProductVM, Product>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.ProductImages.ToList()))
                .ForMember(x => x.Category, opt => opt.MapFrom(src => src.Category));
        }
    }
}
