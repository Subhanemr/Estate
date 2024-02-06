using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record IncludeBlogCommnetVM(int Id, string Comment, DateTime CreateAt, string CreatedBy,
        string AppUserId, IncludeAppUserVM? AppUser, int BlogId, ICollection<IncludeBlogReply> Replies);
}

