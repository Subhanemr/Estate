using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class ProductImageProfile : Profile
    {
        public ProductImageProfile()
        {
            CreateMap<IncludeProductImageVM, ProductImage>().ReverseMap();
        }
    }
}
