using Estate.Application.ViewModels.Product;

namespace Estate.Application.ViewModels.RoofType
{
    public record GetRoofTypeVM(int Id, string Name, ICollection<IncludeProductVM> Products);
}
