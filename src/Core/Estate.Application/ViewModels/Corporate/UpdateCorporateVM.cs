using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record UpdateCorporateVM(string Name, string CorporateLink, string Img, string Description)
    {
        public IFormFile? Photo { get; set; }
    }
}
