namespace Estate.Application.ViewModels
{
    public record IncludeBlogReply
    {
        public int Id { get; init; }
        public string ReplyComment { get; init; }
        public DateTime CreateAt { get; init; }
        public string CreatedBy { get; init; }
        public string AppUserId { get; init; }

        public int BlogCommnetId { get; init; }
        public IncludeAppUserVM? AppUser { get; init; }
    }
}
