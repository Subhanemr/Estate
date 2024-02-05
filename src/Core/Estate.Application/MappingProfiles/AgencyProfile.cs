using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class AgencyProfile : Profile
    {
        public AgencyProfile()
        {
            CreateMap<Agency, CreateAgencyVM>().ReverseMap();
            CreateMap<Agency, UpdateAgencyVM>().ReverseMap();
            CreateMap<GetAgencyVM, Agency>().ReverseMap()
                .ForMember(x => x.AppUsers, opt => opt.MapFrom(src => src.AgencyAppUsers.Select(ma => ma.AppUser).ToList()));
            CreateMap<IncludeAgencyVM, Agency>().ReverseMap();
            CreateMap<ItemAgencyVM, Agency>().ReverseMap()
                .ForMember(x => x.AppUsers, opt => opt.MapFrom(src => src.AgencyAppUsers.Select(ma => ma.AppUser).ToList()));
            CreateMap<UpdateAgencyVM, Agency>().ReverseMap();
        }
    }
}
