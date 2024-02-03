using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record IncludeProductCommentVM(int Id, string Comment, DateTime CoomentTime,
        string AppUserId, AppUser? AppUser, int ProductId, ICollection<IncludeProductReplyVM> Replies);
}
