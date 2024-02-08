namespace Estate.Application.ViewModels
{
    public record ItemBlogVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CreatedBy { get; init; }
        public DateTime CreateAt { get; init; }
        public ICollection<IncludeBlogImageVM> Images { get; init; }
    }
}
