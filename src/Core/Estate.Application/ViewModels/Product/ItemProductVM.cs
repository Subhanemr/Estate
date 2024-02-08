namespace Estate.Application.ViewModels
{
    public record ItemProductVM(int Id, string Name, double Price, float LotSize, float Area, short YearBuilt,
        int CategoryId, IncludeCategoryVM Category, ICollection<IncludeProductImageVM> Images)
    {
        public string OrderDayOrMoth { get; init; }
    }
}
