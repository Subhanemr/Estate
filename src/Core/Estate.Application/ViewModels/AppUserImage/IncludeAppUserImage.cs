namespace Estate.Application.ViewModels
{
    public record IncludeAppUserImage
    {
        public int Id { get; init; }
        public string Url { get; init; }
        public bool? IsPrimary { get; init; }
        public string AppUserId { get; init; }
    }
}
