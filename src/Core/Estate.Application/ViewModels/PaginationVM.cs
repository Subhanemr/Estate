namespace Estate.Application.ViewModels
{
    public class PaginationVM<T> 
    {
        public int CurrentPage { get; set; }
        public double TotalPage { get; set; }
        public ICollection<T>? Items { get; set; }
        public T? Item { get; set; }
    }
}
