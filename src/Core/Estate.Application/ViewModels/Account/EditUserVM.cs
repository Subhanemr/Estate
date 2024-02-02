using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels.Account
{
    public record EditUserVM(string UserName, string? Img, IFormFile? Photo);
}
