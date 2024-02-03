namespace Estate.Application.ViewModels
{
    public record ItemRoofTypeVM(int Id, string Name, ICollection<IncludeProductVM>? Products);
}
