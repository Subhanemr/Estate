using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels.Account
{
    public record EditVM(string UserName, string? Img, IFormFile? Photo, string? Password, string? NewPassword, string? NewConfirmPassword);
}
