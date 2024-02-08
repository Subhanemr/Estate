namespace Estate.Application.ViewModels
{
    public record ItemViewTypeVM(int Id, string Name)
    {
        public ICollection<IncludeProductVM>? Products { get; init; }

    }
}
