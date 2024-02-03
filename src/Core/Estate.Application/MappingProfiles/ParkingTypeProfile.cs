using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class ParkingTypeProfile : Profile
    {
        public ParkingTypeProfile()
        {
            CreateMap<CreateParkingTypeVM, ParkingType>().ReverseMap();
            CreateMap<UpdateParkingTypeVM, ParkingType>().ReverseMap();
            CreateMap<GetParkingTypeVM, ParkingType>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductParkingTypes));
            CreateMap<IncludeParkingTypeVM, ParkingType>().ReverseMap();
            CreateMap<ItemParkingTypeVM, ParkingType>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.ProductParkingTypes));
        }
    }
}
