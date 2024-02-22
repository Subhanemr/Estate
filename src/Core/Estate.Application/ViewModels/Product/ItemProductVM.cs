namespace Estate.Application.ViewModels
{
    public record ItemProductVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public double Price { get; init; }
        public float LotSize { get; init; }
        public float Area { get; init; }
        public int Bedrooms { get; init; }
        public int Bathrooms { get; init; }
        public int Garages { get; init; }
        public int YearBuilt { get; init; }
        public int RoomCount { get; init; }
        public string Description { get; init; }
        public int CategoryId { get; init; }

        public string OrderDayOrMoth { get; init; }
        public IncludeCategoryVM Category { get; init; }
        public ICollection<IncludeProductImageVM> Images { get; init; }
    }
}
