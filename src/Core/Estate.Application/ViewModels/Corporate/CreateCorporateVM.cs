using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels.Corporate
{
    public record CreateCorporateVM(string CorporateLink, string Description, IFormFile Photo);
}
