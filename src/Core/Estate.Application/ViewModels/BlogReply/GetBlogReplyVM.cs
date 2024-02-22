using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record GetBlogReplyVM
    {
        public int Id { get; init; }
        public string ReplyComment { get; init; }
        public DateTime CreateAt { get; init; }
        public string CreatedBy { get; init; }
        public string AppUserId { get; init; }
        public IncludeAppUserVM AppUser { get; init; }
        public int BlogCommnetId { get; init; }
        public IncludeBlogCommnetVM BlogComment { get; init; }
    }
}
