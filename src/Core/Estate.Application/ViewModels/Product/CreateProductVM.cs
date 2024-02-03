using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels
{
    public record CreateProductVM(string Name, double Price, float LotSize, float Area, int Bedrooms, int Bathrooms,
        int Garages, int YearBuilt, int RoomCount, string? SchoolDistrict, string? SchoolDistrictPhone,
        string Address, string Description, string FaceLink, string TwitLink, string? GoogleLink, 
        bool HighSchool, bool MiddleSchool, bool ElementarySchool, int CategoryId, 
        IFormFile MainPhoto, ICollection<IFormFile>? Photos)
    {
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
