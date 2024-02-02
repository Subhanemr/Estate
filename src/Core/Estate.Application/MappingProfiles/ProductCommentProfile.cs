using AutoMapper;
using Estate.Application.ViewModels.ProductComment;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class ProductCommentProfile : Profile
    {
        public ProductCommentProfile()
        {
            CreateMap<CreateProductCommentVM, ProductComment>().ReverseMap();
            CreateMap<UpdateProductCommentVM, ProductComment>().ReverseMap();
            CreateMap<GetProductCommentVM, ProductComment>().ReverseMap()
                .ForMember(x => x.Replies, opt => opt.MapFrom(src => src.ProductReplies.ToList()))
                .ForMember(x => x.Product, opt => opt.MapFrom(src => src.Product))
                .ForMember(x => x.AppUser, opt => opt.MapFrom(src => src.AppUser));
            CreateMap<IncludeProductCommentVM, ProductComment>().ReverseMap()
                .ForMember(x => x.Replies, opt => opt.MapFrom(src => src.ProductReplies.ToList()))
                .ForMember(x => x.AppUser, opt => opt.MapFrom(src => src.AppUser));
            CreateMap<ItemProductCommentVM, ProductComment>().ReverseMap()
                .ForMember(x => x.Replies, opt => opt.MapFrom(src => src.ProductReplies.ToList()))
                .ForMember(x => x.Product, opt => opt.MapFrom(src => src.Product))
                .ForMember(x => x.AppUser, opt => opt.MapFrom(src => src.AppUser));
        }
    }
}
