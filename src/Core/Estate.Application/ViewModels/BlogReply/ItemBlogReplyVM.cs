using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record ItemBlogReplyVM(int Id, string Comment, DateTime CreateAt, string CreatedBy,
        string AppUserId, IncludeAppUserVM AppUser, int BlogCommnetId);
}
