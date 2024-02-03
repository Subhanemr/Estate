using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record CreateCategoryVM(string Name, IFormFile Photo);
}
