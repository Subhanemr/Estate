using Estate.Application.ViewModels.BlogComment;
using Estate.Application.ViewModels.BlogImage;

namespace Estate.Application.ViewModels.Blog
{
    public record GetBlogVM(int Id, string Name, string Description, DateTime DateTime,
        string FaceLink, string TwitLink, string? GoogleLink, string LinkedLink, string InstaLink, 
        ICollection<IncludeBlogImageVM> Images, ICollection<IncludeBlogCommnetVM> Commnets);
}
