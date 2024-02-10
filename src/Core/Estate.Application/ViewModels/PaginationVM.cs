namespace Estate.Application.ViewModels
{
    public class PaginationVM<T> 
    {
        public int Take { get; set; }
        public int? CategoryId { get; set; }
        public int Order { get; set; }
        public string? Search { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinArea { get; set; }
        public int? MaxArea { get; set; }
        public int? MinBeds { get; set; }
        public int? MinBaths { get; set; }
        public int CurrentPage { get; set; }
        public double TotalPage { get; set; }
        public ICollection<T>? Items { get; set; }
        public T? Item { get; set; }
    }
}
