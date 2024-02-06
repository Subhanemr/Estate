using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record ItemBlogCommentVM(int Id, string Comment, DateTime CreateAt, string CreatedBy,
        string AppUserId, IncludeAppUserVM AppUser, int BlogId, IncludeBlogVM? Blog, ICollection<IncludeBlogReply>? Replies);
}
