namespace Estate.Application.ViewModels
{
    public record GetExteriorTypeVM(int Id, string Name, ICollection<IncludeProductVM> Products);
}
