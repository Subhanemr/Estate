namespace Estate.Application.ViewModels
{
    public record IncludeCategoryVM(int Id,string Name, string Img)
    {
        public ICollection<IncludeProductVM> Products { get; set; }
    }
}
