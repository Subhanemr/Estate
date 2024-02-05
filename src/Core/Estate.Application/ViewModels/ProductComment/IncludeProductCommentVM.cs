using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record IncludeProductCommentVM(int Id, string Comment, DateTime CoomentTime,
        string AppUserId, IncludeAppUserVM? AppUser, int ProductId, ICollection<IncludeProductReplyVM> Replies);
}
