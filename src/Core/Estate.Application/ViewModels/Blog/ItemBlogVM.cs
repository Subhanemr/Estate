namespace Estate.Application.ViewModels
{
    public record ItemBlogVM(int Id, string Name, string Description, DateTime DateTime, ICollection<IncludeBlogImageVM> Images);
}
