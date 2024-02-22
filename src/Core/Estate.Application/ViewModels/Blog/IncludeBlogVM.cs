namespace Estate.Application.ViewModels
{
    public record IncludeBlogVM
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string FaceLink { get; init; }
        public string TwitLink { get; init; }
        public string? GoogleLink { get; init; }
        public string LinkedLink { get; init; }
        public string InstaLink { get; init; }
        public string CreatedBy { get; init; }
        public DateTime CreateAt { get; init; }

        public ICollection<IncludeBlogImageVM> Images { get; init; }
    }
}
