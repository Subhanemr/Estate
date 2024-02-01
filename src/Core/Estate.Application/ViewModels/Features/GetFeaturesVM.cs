using Estate.Application.ViewModels.Product;

namespace Estate.Application.ViewModels.Features
{
    public record GetFeaturesVM(int Id, string Name, ICollection<IncludeProductVM> Products);
}
