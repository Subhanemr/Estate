using Estate.Application.ViewModels.BlogImage;
using Estate.Domain.Entities;

namespace Estate.Application.ViewModels.Blog
{
    public record IncludeBlogVM(int Id, string Name, string Description, DateTime DateTime, 
        string FaceLink, string TwitLink, string? GoogleLink, string LinkedLink, string InstaLink, ICollection<IncludeBlogImageVM> Images);
}
