using Estate.Application.ViewModels.Product;
using Estate.Application.ViewModels.ProductReply;
using Estate.Domain.Entities;

namespace Estate.Application.ViewModels.ProductComment
{
    public record ItemProductCommentVM(int Id, string Comment, DateTime CoomentTime,
        string AppUserId, AppUser AppUser, int ProductId, IncludeProductVM Product, ICollection<IncludeProductReplyVM>? Replies);
}
