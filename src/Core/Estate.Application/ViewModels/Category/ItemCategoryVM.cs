using Estate.Application.ViewModels.Product;

namespace Estate.Application.ViewModels.Category
{
    public record ItemCategoryVM(int Id, string Name, string Img, ICollection<IncludeProductVM> Products);
}
