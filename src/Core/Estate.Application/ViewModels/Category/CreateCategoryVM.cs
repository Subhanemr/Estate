using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels.Category
{
    public record CreateCategoryVM(string Name, IFormFile Photo);
}
