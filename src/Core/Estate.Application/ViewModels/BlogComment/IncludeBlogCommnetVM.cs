using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record IncludeBlogCommnetVM(int Id, string Comment, DateTime CreateAt, string CreatedBy,
        string AppUserId, int BlogId)
    {
        public ICollection<IncludeBlogReply>? Replies { get; init; }
        public IncludeAppUserVM? AppUser { get; init; }
    }
}

