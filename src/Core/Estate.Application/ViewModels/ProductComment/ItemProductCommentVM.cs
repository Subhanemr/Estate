using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record ItemProductCommentVM
    {
        public int Id { get; init; }
        public string Comment { get; init; }
        public DateTime CreateAt { get; init; }
        public string CreatedBy { get; init; }
        public string AppUserId { get; init; }
        public IncludeAppUserVM AppUser { get; init; }
        public int ProductId { get; init; }
        public IncludeProductVM Product { get; init; }
        public ICollection<IncludeProductReplyVM> Replies { get; init; }
    }
}
