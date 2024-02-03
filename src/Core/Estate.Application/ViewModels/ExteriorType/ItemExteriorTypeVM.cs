namespace Estate.Application.ViewModels
{
    public record ItemExteriorTypeVM(int Id, string Name, ICollection<IncludeProductVM>? Products);
}
