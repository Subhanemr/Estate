using Estate.Application.ViewModels.Product;

namespace Estate.Application.ViewModels.Features
{
    public record ItemFeaturesVM(int Id, string Name, ICollection<IncludeProductVM>? Products);
}
