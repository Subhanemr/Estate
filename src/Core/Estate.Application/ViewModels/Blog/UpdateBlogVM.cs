using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record UpdateBlogVM
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string FaceLink { get; init; }
        public string TwitLink { get; init; }
        public string? GoogleLink { get; init; }
        public string LinkedLink { get; init; }
        public string InstaLink { get; init; }

        public IFormFile? MainPhoto { get; init; }
        public IFormFile? HoverPhoto { get; init; }

        public ICollection<IncludeBlogImageVM>? Images { get; set; }
    }
}
