namespace Estate.Application.ViewModels
{
    public record GetCategoryVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Img { get; init; }
        public ICollection<IncludeProductVM> Products { get; init; }
    }
}
