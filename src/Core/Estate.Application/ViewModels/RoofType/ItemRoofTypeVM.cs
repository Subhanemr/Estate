namespace Estate.Application.ViewModels
{
    public record ItemRoofTypeVM(int Id, string Name)
    {
        public ICollection<IncludeProductVM>? Products { get; init; }
    }
}
