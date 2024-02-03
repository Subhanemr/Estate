using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record CreateCorporateVM(string CorporateLink, string Description, IFormFile Photo);
}
