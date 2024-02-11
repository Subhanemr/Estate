using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record IncludeProductCommentVM(int Id, string Comment, DateTime CreateAt, string CreatedBy,
        string AppUserId, int ProductId)
    {
        
        public ICollection<IncludeProductReplyVM>? Replies { get; init; }
        public IncludeAppUserVM? CommentUser { get; init; }
    }
}
