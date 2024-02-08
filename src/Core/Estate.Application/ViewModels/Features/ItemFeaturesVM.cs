namespace Estate.Application.ViewModels
{
    public record ItemFeaturesVM(int Id, string Name)
    {
        public ICollection<IncludeProductVM>? Products { get; init; }
    }
}
