using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record GetProductCommentVM(int Id, string Comment, DateTime CreateAt, string CreatedBy,
        string AppUserId, IncludeAppUserVM AppUser, int ProductId, IncludeProductVM Product, ICollection<IncludeProductReplyVM> Replies);
}
