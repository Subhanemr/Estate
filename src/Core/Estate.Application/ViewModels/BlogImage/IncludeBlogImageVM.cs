namespace Estate.Application.ViewModels
{
    public record IncludeBlogImageVM
    {
        public int Id { get; init; }
        public string Url { get; init; }
        public bool? IsPrimary { get; init; }
        public int BlogId { get; init; }
    }
}
