namespace Estate.Application.ViewModels
{
    public record ItemFeaturesVM
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public ICollection<IncludeProductVM>? Products { get; init; }
    }
}
