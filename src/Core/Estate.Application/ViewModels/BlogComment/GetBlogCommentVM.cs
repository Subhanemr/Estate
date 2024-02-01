using Estate.Application.ViewModels.Blog;
using Estate.Domain.Entities;

namespace Estate.Application.ViewModels.BlogComment
{
    public record GetBlogCommentVM(int Id, string Comment, DateTime CoomentTime, string AppUserId, AppUser AppUser, int BlogId, IncludeBlogVM Blog);
}
