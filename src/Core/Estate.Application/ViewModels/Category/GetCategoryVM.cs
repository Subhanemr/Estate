namespace Estate.Application.ViewModels
{
    public record GetCategoryVM(int Id, string Name, string Img, ICollection<IncludeProductVM> Products);
}
