using Estate.Application.ViewModels.Agency;
using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels.Account
{
    public record CreateAppUserAgent(string PhoneFirst, string? PhoneSecond, string Address, string About, 
        string FaceLink, string TwitLink, string? GoogleLink, string LinkedLink, string InstaLink, IFormFile MainPhoto,
        ICollection<IFormFile> Photos, bool TermsConditions)
    {
        public ICollection<IncludeAgencyVM>? Agencys { get; set; }
        public List<int>? AgencyIds { get; set; }
    }
}
