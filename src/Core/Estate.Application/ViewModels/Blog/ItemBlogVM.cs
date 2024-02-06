namespace Estate.Application.ViewModels
{
    public record ItemBlogVM(int Id, string Name, string Description, string CreatedBy,
        DateTime CreateAt, ICollection<IncludeBlogImageVM> Images);
}
