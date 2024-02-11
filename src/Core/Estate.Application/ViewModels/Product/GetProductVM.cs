namespace Estate.Application.ViewModels
{
    public record GetProductVM(int Id, string Name, DateTime CreateAt, double Price, float LotSize, float Area, byte Bedrooms, byte Bathrooms,
        byte Garages, short YearBuilt, byte RoomCount, string? SchoolDistrict, string? SchoolDistrictPhone,
        string Address, string Description, string FaceLink, string TwitLink, string? GoogleLink,
        bool HighSchool, bool MiddleSchool, bool ElementarySchool, int CategoryId)
    {
        public string OrderDayOrMoth { get; init; }
        public List<IncludeViewTypeVM>? ViewTypes { get; init; }
        public IncludeAppUserVM AppUser { get; init; }
        public List<IncludeRoofTypeVM>? RoofTypes { get; init; }
        public List<IncludeParkingTypeVM>? ParkingTypes { get; init; }
        public List<IncludeExteriorTypeVM>? ExteriorTypes { get; init; }
        public List<IncludeProductCommentVM>? Comments { get; init; }
        public List<IncludeFeaturesVM>? Features { get; init; }
        public ICollection<IncludeProductImageVM> Images { get; init; }
        public IncludeCategoryVM Category { get; init; }
    }
}
