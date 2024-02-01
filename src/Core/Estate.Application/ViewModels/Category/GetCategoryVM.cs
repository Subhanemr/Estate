using Estate.Application.ViewModels.Product;
using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels.Category
{
    public record GetCategoryVM(int Id, string Name, string Img, ICollection<IncludeProductVM> Products);
}
