namespace Estate.Domain.Entities
{
    public class Blog : BaseNameEntity
    {
        public string Description { get; set; } = null!;
        public DateTime DateTime { get; set; }

        public string FaceLink { get; set; } = null!;
        public string TwitLink { get; set; } = null!;
        public string? GoogleLink { get; set; }
        public string LinkedLink { get; set; } = null!;
        public string InstaLink { get; set; } = null!;

        public ICollection<BlogImage> BlogImages { get; set; } = null!;
        public ICollection<BlogComment>? BlogComments { get; set; }

    }
}
