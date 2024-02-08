using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record CreateProductVM
    {
        public string Name { get; init; }
        public double Price { get; init; }
        public float LotSize { get; init; }
        public float Area { get; init; }
        public int Bedrooms { get; init; }
        public int Bathrooms { get; init; }
        public int Garages { get; init; }
        public int YearBuilt { get; init; }
        public int RoomCount { get; init; }
        public string? SchoolDistrict { get; init; }
        public string? SchoolDistrictPhone { get; init; }
        public string Address { get; init; }
        public string Description { get; init; }
        public string FaceLink { get; init; }
        public string TwitLink { get; init; }
        public string? GoogleLink { get; init; }
        public bool HighSchool { get; init; }
        public bool MiddleSchool { get; init; }
        public bool ElementarySchool { get; init; }
        public int CategoryId { get; init; }
        public IFormFile MainPhoto { get; init; }
        public ICollection<IFormFile>? Photos { get; init; }


        public ICollection<IncludeCategoryVM>? Categories { get; set; }

        public ICollection<IncludeFeaturesVM>? Features { get; set; }
        public List<int> FeatureIds { get; set; }

        public ICollection<IncludeExteriorTypeVM>? ExteriorTypes { get; set; }
        public List<int> ExteriorTypeIds { get; set; }

        public ICollection<IncludeParkingTypeVM>? ParkingTypes { get; set; }
        public List<int> ParkingTypeIds { get; set; }

        public ICollection<IncludeRoofTypeVM>? RoofTypes { get; set; }
        public List<int> RoofTypeIds { get; set; }

        public ICollection<IncludeViewTypeVM>? ViewTypes { get; set; }
        public List<int> ViewTypeIds { get; set; }
    }
}
