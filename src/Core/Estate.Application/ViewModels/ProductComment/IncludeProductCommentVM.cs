using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record IncludeProductCommentVM(int Id, string Comment, DateTime CreateAt, string CreatedBy,
        string AppUserId, IncludeAppUserVM? AppUser, int ProductId, ICollection<IncludeProductReplyVM> Replies);
}
