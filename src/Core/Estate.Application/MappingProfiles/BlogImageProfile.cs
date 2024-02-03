using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class BlogImageProfile : Profile
    {
        public BlogImageProfile()
        {
            CreateMap<IncludeBlogImageVM, BlogImage>().ReverseMap();
        }
    }
}
