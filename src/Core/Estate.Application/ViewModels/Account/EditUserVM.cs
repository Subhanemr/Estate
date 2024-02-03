using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record EditUserVM(string UserName, string? Img, IFormFile? Photo);
}
