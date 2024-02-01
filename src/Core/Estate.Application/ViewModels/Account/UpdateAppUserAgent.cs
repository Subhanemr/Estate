using Estate.Application.ViewModels.Agency;
using Estate.Application.ViewModels.AppUserImage;
using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels.Account
{
    public record UpdateAppUserAgent(string PhoneFirst, string? PhoneSecond, string Address, string About,
        string FaceLink, string TwitLink, string? GoogleLink, string LinkedLink, string InstaLink, IFormFile MainPhoto,
        ICollection<IFormFile>? Photos, bool TermsConditions)
    {
        public ICollection<IncludeAppUserImage>? AppUserImages { get; set; }
        public List<int>? AppUserImageIds { get; set; }

        public ICollection<IncludeAgencyVM>? Agencys { get; set; }
        public List<int>? AgencyIds { get; set; }
    }
}
