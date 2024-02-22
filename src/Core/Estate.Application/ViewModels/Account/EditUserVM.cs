using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record EditUserVM
    {
        public string UserName { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string? Img { get; init; }

        public IFormFile? Photo { get; init; }
    }
}
