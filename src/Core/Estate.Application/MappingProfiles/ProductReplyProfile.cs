using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;

namespace Estate.Application.MappingProfiles
{
    internal class ProductReplyProfile : Profile
    {
        public ProductReplyProfile()
        {
            CreateMap<CreateProductReplyVM, ProductReply>().ReverseMap();
            CreateMap<UpdateProductReplyVM, ProductReply>().ReverseMap();
            CreateMap<GetProductReplyVM, ProductReply>().ReverseMap()
                .ForMember(x => x.ProductComment, opt => opt.MapFrom(src => src.ProductComment))
                .ForMember(x => x.AppUser, opt => opt.MapFrom(src => src.AppUser));
            CreateMap<IncludeProductReplyVM, ProductReply>().ReverseMap()
                .ForMember(x => x.ReplyUser, opt => opt.MapFrom(src => src.AppUser));
            CreateMap<ItemProductReplyVM, ProductReply>().ReverseMap()
                .ForMember(x => x.AppUser, opt => opt.MapFrom(src => src.AppUser));
        }
    }
}
