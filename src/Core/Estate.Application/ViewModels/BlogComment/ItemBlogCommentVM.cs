using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record ItemBlogCommentVM
    {
        public int Id { get; init; }
        public string Comment { get; init; }
        public DateTime CreateAt { get; init; }
        public string CreatedBy { get; init; }
        public string AppUserId { get; init; }
        public IncludeAppUserVM AppUser { get; init; }
        public int BlogId { get; init; }
        public IncludeBlogVM Blog { get; init; }
        public ICollection<IncludeBlogReply> Replies { get; init; }
    }
}
