using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record GetProductReplyVM(int Id, string Comment, DateTime CoomentTime,
        string AppUserId, IncludeAppUserVM AppUser, int ProductCommentId, IncludeProductCommentVM ProductComment);
}
