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
            CreateMap<IncludeAppUserVM, AppUser>().ReverseMap()
                .ForMember(x => x.AgencyUser, opt => opt.MapFrom(src => src.Agency));
            CreateMap<CreateAppUserAgentVM, AppUser>().ReverseMap();
            CreateMap<UpdateAppUserAgentVM, AppUser>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.AppUserImages.ToList()));
            CreateMap<GetAppUserVM, AppUser>().ReverseMap()
                .ForMember(x => x.Favorites, opt => opt.MapFrom(src => src.Favorites.Select(a => a.Product).ToList()))
                .ForMember(x => x.Products, opt => opt.MapFrom(src => src.Products.ToList()))
                .ForMember(x => x.Agency, opt => opt.MapFrom(src => src.Agency))
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.AppUserImages.ToList()));
            CreateMap<IncludeAppUserVM, AppUser>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.AppUserImages.ToList()));
            CreateMap<ItemAppUserVM, AppUser>().ReverseMap();

        }
    }
}
