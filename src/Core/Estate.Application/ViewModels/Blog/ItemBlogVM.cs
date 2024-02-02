using Estate.Application.ViewModels.BlogImage;

namespace Estate.Application.ViewModels.Blog
{
    public record ItemBlogVM(int Id, string Name, string Description, DateTime DateTime, ICollection<IncludeBlogImageVM> Images);
}
