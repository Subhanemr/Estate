using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record GetBlogCommentVM(int Id, string Comment, DateTime CoomentTime, 
        string AppUserId, AppUser AppUser, int BlogId, IncludeBlogVM Blog, ICollection<IncludeBlogReply> Replies);
}
