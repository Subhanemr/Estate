using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record UpdateCategoryVM
    {
        public string Name { get; init; }
        public string? Img { get; init; }

        public IFormFile? Photo { get; set; }
    }
}
