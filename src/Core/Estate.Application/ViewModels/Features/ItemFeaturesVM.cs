namespace Estate.Application.ViewModels
{
    public record ItemFeaturesVM(int Id, string Name, ICollection<IncludeProductVM>? Products);
}
