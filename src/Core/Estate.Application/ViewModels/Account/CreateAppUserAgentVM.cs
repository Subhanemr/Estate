using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Estate.Application.ViewModels
{
    public record CreateAppUserAgentVM
    {
        public string? Id { get; set; }

        public string PhoneNumber { get; init; }
        public string? PhoneSecond { get; init; }
        public string Address { get; init; }
        public string About { get; init; }
        public string FaceLink { get; init; }
        public string TwitLink { get; init; }
        public string? GoogleLink { get; init; }
        public string LinkedLink { get; init; }
        public string InstaLink { get; init; }
        public bool TermsConditions { get; init; }

        public IFormFile MainPhoto { get; init; }
        public ICollection<IFormFile> Photos { get; init; }

        public ICollection<IncludeAgencyVM>? Agencys { get; set; }
        public List<int>? AgencyIds { get; set; }
    }
}
