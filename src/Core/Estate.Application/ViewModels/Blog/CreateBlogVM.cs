using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record CreateBlogVM(string Name, string Description, 
        string FaceLink, string TwitLink, string? GoogleLink, string LinkedLink, string InstaLink, IFormFile MainPhoto,
        ICollection<IFormFile>? Photos);
}
