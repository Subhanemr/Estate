namespace Estate.Application.ViewModels
{
    public record IncludeProductImageVM
    {
        public int Id { get; init; }
        public string Url { get; init; }
        public bool? IsPrimary { get; init; }
        public int ProductId { get; init; }
    }
}
