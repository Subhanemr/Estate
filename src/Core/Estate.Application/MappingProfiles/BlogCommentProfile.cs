﻿using AutoMapper;
using Estate.Application.ViewModels.BlogComment;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class BlogCommentProfile : Profile
    {
        public BlogCommentProfile()
        {
            CreateMap<CreateBlogCommentVM, BlogComment>().ReverseMap();
            CreateMap<UpdateBlogCommentVM, BlogComment>().ReverseMap();
            CreateMap<GetBlogCommentVM, BlogComment>().ReverseMap()
                .ForMember(x => x.Replies, opt => opt.MapFrom(src => src.BlogReplies.ToList()))
                .ForMember(x => x.Blog, opt => opt.MapFrom(src => src.Blog))
                .ForMember(x => x.AppUser, opt => opt.MapFrom(src => src.AppUser));
            CreateMap<IncludeBlogCommnetVM, BlogComment>().ReverseMap()
                .ForMember(x => x.AppUser, opt => opt.MapFrom(src => src.AppUser));
            CreateMap<ItemBlogCommentVM, BlogComment>().ReverseMap()
                .ForMember(x => x.Replies, opt => opt.MapFrom(src => src.BlogReplies.ToList()))
                .ForMember(x => x.Blog, opt => opt.MapFrom(src => src.Blog))
                .ForMember(x => x.AppUser, opt => opt.MapFrom(src => src.AppUser));
        }
    }
}
