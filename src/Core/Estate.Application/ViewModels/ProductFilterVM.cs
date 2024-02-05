namespace Estate.Application.ViewModels
{
    public class ProductFilterVM
    {
        public ICollection<ItemProductVM> Products { get; set; }
        public ICollection<IncludeCategoryVM> Categories { get; set; }
    }
}
