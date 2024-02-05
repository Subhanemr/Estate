using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record ItemProductCommentVM(int Id, string Comment, DateTime CoomentTime,
        string AppUserId, IncludeAppUserVM AppUser, int ProductId, IncludeProductVM Product, ICollection<IncludeProductReplyVM>? Replies);
}
