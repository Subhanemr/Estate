using Estate.Application.ViewModels.Product;

namespace Estate.Application.ViewModels.ViewType
{
    public record ItemViewTypeVM(int Id, string Name, ICollection<IncludeProductVM>? Products);
}
