using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record UpdateCorporateVM(string CorporateLink, string Description, IFormFile? Photo);
}
