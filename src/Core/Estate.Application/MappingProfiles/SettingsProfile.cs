using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class SettingsProfile : Profile
    {
        public SettingsProfile()
        {
            CreateMap<ItemSettingsVM, Settings>().ReverseMap();
        }
    }
}
