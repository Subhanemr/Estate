using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record IncludeProductReplyVM
    {
        public int Id { get; init; }
        public string ReplyComment { get; init; }
        public DateTime CreateAt { get; init; }
        public string CreatedBy { get; init; }
        public string AppUserId { get; init; }

        public int ProductCommentId { get; init; }
        public IncludeAppUserVM? ReplyUser { get; init; }
    }
}
