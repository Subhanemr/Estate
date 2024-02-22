using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record IncludeProductCommentVM
    {
        public int Id { get; init; }
        public string Comment { get; init; }
        public DateTime CreateAt { get; init; }
        public string CreatedBy { get; init; }
        public string AppUserId { get; init; }
        public int ProductId { get; init; }

        public ICollection<IncludeProductReplyVM>? Replies { get; init; }
        public IncludeAppUserVM? CommentUser { get; init; }
    }
}
