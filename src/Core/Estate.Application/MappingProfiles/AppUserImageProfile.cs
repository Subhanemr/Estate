using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class AppUserImageProfile : Profile
    {
        public AppUserImageProfile()
        {
            CreateMap<IncludeAppUserImage, AppUserImage>().ReverseMap();
        }
    }
}
