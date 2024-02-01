using Estate.Application.ViewModels.Product;

namespace Estate.Application.ViewModels.ViewType
{
    public record GetViewTypeVM(int Id, string Name, ICollection<IncludeProductVM> Products);
}
