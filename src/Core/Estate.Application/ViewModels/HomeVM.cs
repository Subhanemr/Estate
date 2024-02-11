namespace Estate.Application.ViewModels
{
    public record HomeVM
    {
        public PaginationVM<ProductFilterVM> Pagination { get; set; }
        public ICollection<ItemAppUserVM> Agents { get; set; }
    }
}
