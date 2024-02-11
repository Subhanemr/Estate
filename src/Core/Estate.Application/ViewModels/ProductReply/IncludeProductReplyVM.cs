using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record IncludeProductReplyVM(int Id, string ReplyComment, DateTime CreateAt, string CreatedBy,
        string AppUserId)
    {
        public int ProductCommentId { get; init; }
        public IncludeAppUserVM? ReplyUser { get; init; }
    }
}
