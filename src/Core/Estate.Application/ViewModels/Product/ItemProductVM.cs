namespace Estate.Application.ViewModels
{
    public record ItemProductVM(int Id, string Name, double Price, float LotSize, float Area, short YearBuilt,
        int CategoryId)
    {
        public string OrderDayOrMoth { get; init; }
        public IncludeCategoryVM Category { get; init; }
        public ICollection<IncludeProductImageVM> Images { get; init; }
    }
}
