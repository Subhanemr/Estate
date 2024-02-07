using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record EditUserVM(string UserName, string Name, string Surname, string? Img)
    {
        public string? Id { get; init; }
        public IFormFile? Photo { get; init; }
    }
}
