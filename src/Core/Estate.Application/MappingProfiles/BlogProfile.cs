using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<CreateBlogVM, Blog>().ReverseMap();
            CreateMap<UpdateBlogVM, Blog>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.BlogImages.ToList()));
            CreateMap<GetBlogVM, Blog>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.BlogImages.ToList()))
                .ForMember(x => x.Commnets, opt => opt.MapFrom(src => src.BlogComments.ToList()));
            CreateMap<IncludeBlogVM, Blog>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.BlogImages.ToList()));
            CreateMap<ItemBlogVM, Blog>().ReverseMap()
                .ForMember(x => x.Images, opt => opt.MapFrom(src => src.BlogImages.ToList()));
            CreateMap<CreateBlogVM, Blog>().ReverseMap();
        }
    }
}
