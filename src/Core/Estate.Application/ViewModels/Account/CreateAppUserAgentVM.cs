using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record CreateAppUserAgentVM(string PhoneFirst, string? PhoneSecond, string Address, string About, 
        string FaceLink, string TwitLink, string? GoogleLink, string LinkedLink, string InstaLink, IFormFile MainPhoto,
        ICollection<IFormFile> Photos, bool TermsConditions)
    {
        public ICollection<IncludeAgencyVM>? Agencys { get; set; }
        public List<int>? AgencyIds { get; set; }
    }
}
