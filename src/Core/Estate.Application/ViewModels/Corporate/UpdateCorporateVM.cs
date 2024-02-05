using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record UpdateCorporateVM(string CorporateLink, string Description)
    {
        public IFormFile? Photo { get; set; }
    }
}
