using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record GetProductReplyVM(int Id, string Comment, DateTime CreateAt, string CreatedBy,
        string AppUserId, IncludeAppUserVM AppUser, int ProductCommentId, IncludeProductCommentVM ProductComment);
}
