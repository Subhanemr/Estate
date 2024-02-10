namespace Estate.Application.ViewModels
{
    public record ItemProductVM(int Id, string Name, double Price, float LotSize, float Area, short YearBuilt,
        int CategoryId, byte Bedrooms, byte Bathrooms, string Description, byte Garages)
    {
        public string OrderDayOrMoth { get; init; }
        public IncludeCategoryVM Category { get; init; }
        public ICollection<IncludeProductImageVM> Images { get; init; }
    }
}
