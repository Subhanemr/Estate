using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class CorporateProfile : Profile
    {
        public CorporateProfile()
        {
            CreateMap<CreateCorporateVM, Corporate>().ReverseMap();
            CreateMap<UpdateCorporateVM, Corporate>().ReverseMap();
            CreateMap<GetCorporateVM, Corporate>().ReverseMap()
                .ForMember(x => x.Clients, opt => opt.MapFrom(src => src.Clients.ToList()));
            CreateMap<IncludeCorporateVM, Corporate>().ReverseMap();
            CreateMap<ItemCorporateVM, Corporate>().ReverseMap()
                .ForMember(x => x.Clients, opt => opt.MapFrom(src => src.Clients.ToList()));
        }
    }
}
