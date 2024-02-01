using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels.Corporate
{
    public record UpdateCorporateVM(string CorporateLink, string Description, IFormFile? Photo);
}
