namespace Estate.Application.ViewModels
{
    public record GetViewTypeVM(int Id, string Name, ICollection<IncludeProductVM> Products);
}
