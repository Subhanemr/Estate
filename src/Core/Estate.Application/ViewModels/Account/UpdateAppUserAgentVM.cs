using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record UpdateAppUserAgentVM(string PhoneFirst, string? PhoneSecond, string Address, string About,
        string FaceLink, string TwitLink, string? GoogleLink, string LinkedLink, string InstaLink, IFormFile? MainPhoto,
        ICollection<IFormFile>? Photos, bool TermsConditions)
    {
        public ICollection<IncludeAppUserImage>? Images { get; set; }
        public List<int>? ImageIds { get; set; }

        public ICollection<IncludeAgencyVM>? Agencys { get; set; }
        public List<int>? AgencyIds { get; set; }
    }
}
