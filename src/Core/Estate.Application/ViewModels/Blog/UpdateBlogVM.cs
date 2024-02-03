using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record UpdateBlogVM(string Name, string Description,
        string FaceLink, string TwitLink, string? GoogleLink, string LinkedLink, string InstaLink, IFormFile? MainPhoto,
        ICollection<IFormFile>? Photos)
    {
        public ICollection<IncludeBlogImageVM>? Images { get; set; }
        public List<int>? ImageIds { get; set; }
    }
}
