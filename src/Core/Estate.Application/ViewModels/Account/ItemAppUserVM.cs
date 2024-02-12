using Microsoft.AspNetCore.Identity;

namespace Estate.Application.ViewModels
{
    public record ItemAppUserVM(string Id, string Name, string Surname, string UserName, string Img, string? Address, string? About,
        string PhoneNumber, string Email, bool SoulOfAgency);
}
