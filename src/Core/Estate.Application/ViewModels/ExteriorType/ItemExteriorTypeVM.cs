using Estate.Application.ViewModels.Product;

namespace Estate.Application.ViewModels.ExteriorType
{
    public record ItemExteriorTypeVM(int Id, string Name, ICollection<IncludeProductVM>? Products);
}
