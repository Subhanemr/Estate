using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record UpdateBlogVM(string Name, string Description,
        string FaceLink, string TwitLink, string? GoogleLink, string LinkedLink, string InstaLink)
    {
        public IFormFile? MainPhoto { get; init; }
        public IFormFile? HoverPhoto { get; init; }

        public ICollection<IncludeBlogImageVM>? Images { get; set; }
    }
}
