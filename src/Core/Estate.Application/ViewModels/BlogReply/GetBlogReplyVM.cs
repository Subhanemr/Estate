using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record GetBlogReplyVM(int Id, string Comment, DateTime CoomentTime, 
        string AppUserId, AppUser AppUser, int BlogCommnetId, IncludeBlogCommnetVM BlogComment);
}
