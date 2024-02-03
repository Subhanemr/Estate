namespace Estate.Application.ViewModels
{
    public record GetRoofTypeVM(int Id, string Name, ICollection<IncludeProductVM> Products);
}
