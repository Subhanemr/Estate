using AutoMapper;
using Estate.Application.ViewModels.Client;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<CreateClientVM, Client>().ReverseMap();
            CreateMap<UpdateClientVM, Client>().ReverseMap();
            CreateMap<GetClientVM, Client>().ReverseMap()
                .ForMember(x => x.Corporate, opt => opt.MapFrom(src => src.Corporate))
                .ForMember(x => x.AppUser, opt => opt.MapFrom(src => src.AppUser));
            CreateMap<IncludeClientVM, Client>().ReverseMap();
            CreateMap<ItemClientVM, Client>().ReverseMap()
                .ForMember(x => x.Corporate, opt => opt.MapFrom(src => src.Corporate))
                .ForMember(x => x.AppUser, opt => opt.MapFrom(src => src.AppUser));
        }
    }
}
