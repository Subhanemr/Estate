using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record GetBlogReplyVM(int Id, string Comment, DateTime CreateAt, string CreatedBy,
        string AppUserId, IncludeAppUserVM AppUser, int BlogCommnetId, IncludeBlogCommnetVM BlogComment);
}
