namespace Estate.Domain.Entities
{
    public class Blog : BaseNameEntity
    {
        public string Description { get; set; } = null!;
        public DateTime DateTime { get; set; }

        public string? FaceLink { get; set; }
        public string? TwitLink { get; set; }
        public string? GoogleLink { get; set; }
        public string? LinkedLink { get; set; }
        public string? InstaLink { get; set; }

        public ICollection<BlogComment>? BlogComments { get; set; }

    }
}
