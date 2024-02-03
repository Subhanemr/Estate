namespace Estate.Application.ViewModels
{
    public record GetFeaturesVM(int Id, string Name, ICollection<IncludeProductVM> Products);
}
