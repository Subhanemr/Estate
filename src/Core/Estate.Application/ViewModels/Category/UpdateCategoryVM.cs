using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record UpdateCategoryVM(string Name, string? Img)
    {
        public IFormFile? Photo { get; set; }
    }
}
