using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels.Category
{
    public record UpdateCategoryVM(string Name, string Img ,IFormFile? Photo);
}
