namespace Estate.Application.ViewModels
{
    public record ItemViewTypeVM(int Id, string Name, ICollection<IncludeProductVM>? Products);
}
