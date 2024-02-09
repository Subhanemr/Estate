namespace Estate.Application.ViewModels
{
    public record IncludeBlogReply(int Id, string ReplyComment, DateTime CreateAt, string CreatedBy,
        string AppUserId)
    {
        public int BlogCommnetId { get; init; }
        public IncludeAppUserVM? AppUser { get; init; }
    }
}
