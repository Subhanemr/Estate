using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AppUser, LoginVM>().ReverseMap();
            CreateMap<AppUser, RegisterVM>().ReverseMap();
            CreateMap<AppUser, EditUserVM>().ReverseMap();
            CreateMap<IncludeAppUserVM, AppUser>().ReverseMap();
            CreateMap<CreateAppUserAgentVM, AppUser>().ReverseMap();
            CreateMap<UpdateAppUserAgentVM, AppUser>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.AppUserImages.ToList()));
        }
    }
}
