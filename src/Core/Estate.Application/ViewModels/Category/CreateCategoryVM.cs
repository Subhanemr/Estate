using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record CreateCategoryVM
    {
        public string Name { get; init; }
        public IFormFile Photo { get; init; }
    }
}
