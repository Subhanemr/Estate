using Estate.Application.ViewModels.BlogComment;
using Estate.Application.ViewModels.ProductComment;
using Estate.Domain.Entities;

namespace Estate.Application.ViewModels.ProductReply
{
    public record GetProductReplyVM(int Id, string Comment, DateTime CoomentTime,
        string AppUserId, AppUser AppUser, int ProductCommentId, IncludeProductCommentVM ProductComment);
}
