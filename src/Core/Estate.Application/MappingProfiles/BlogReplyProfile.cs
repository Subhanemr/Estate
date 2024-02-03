using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class BlogReplyProfile : Profile
    {
        public BlogReplyProfile()
        {
            CreateMap<CreateBlogReplyVM, BlogReply>().ReverseMap();
            CreateMap<UpdateBlogReplyVM, BlogReply>().ReverseMap();
            CreateMap<GetBlogReplyVM, BlogReply>().ReverseMap()
                .ForMember(x => x.BlogComment, opt => opt.MapFrom(src => src.BlogComment))
                .ForMember(x => x.AppUser, opt => opt.MapFrom(src => src.AppUser));
            CreateMap<IncludeBlogReply, BlogReply>().ReverseMap()
                .ForMember(x => x.AppUser, opt => opt.MapFrom(src => src.AppUser));
            CreateMap<ItemBlogReplyVM, BlogReply>().ReverseMap()
                .ForMember(x => x.AppUser, opt => opt.MapFrom(src => src.AppUser));
        }
    }
}
