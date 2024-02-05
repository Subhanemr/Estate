using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record IncludeProductReplyVM(int Id, string Comment, DateTime CoomentTime,
        string AppUserId, IncludeAppUserVM? AppUser, int ProductCommentId);
}
