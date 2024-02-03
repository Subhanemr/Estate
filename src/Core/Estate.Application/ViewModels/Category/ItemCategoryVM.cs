namespace Estate.Application.ViewModels
{
    public record ItemCategoryVM(int Id, string Name, string Img, ICollection<IncludeProductVM> Products);
}
