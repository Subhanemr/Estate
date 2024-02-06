using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record EditUserVM(string UserName, string Name, string Surname, string? Img, IFormFile? Photo);
}
