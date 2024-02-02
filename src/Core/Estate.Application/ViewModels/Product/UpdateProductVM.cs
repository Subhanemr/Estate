using Estate.Application.ViewModels.Category;
using Estate.Application.ViewModels.ExteriorType;
using Estate.Application.ViewModels.Features;
using Estate.Application.ViewModels.ParkingType;
using Estate.Application.ViewModels.ProductImage;
using Estate.Application.ViewModels.RoofType;
using Estate.Application.ViewModels.ViewType;
using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels.Product
{
    public record UpdateProductVM(string Name, double Price, float LotSize, float Area, int Bedrooms, int Bathrooms,
        int Garages, int YearBuilt, int RoomCount, string? SchoolDistrict, string? SchoolDistrictPhone,
        string Address, string Description, string FaceLink, string TwitLink, string? GoogleLink,
        bool HighSchool, bool MiddleSchool, bool ElementarySchool, int CategoryId,
        IFormFile? MainPhoto, ICollection<IFormFile>? Photos)
    {
        public ICollection<IncludeCategoryVM>? Categories { get; set; }

        public ICollection<IncludeProductImageVM>? Images { get; set; }
        public List<int>? ImageIds { get; set; }

        public ICollection<IncludeFeaturesVM>? Features { get; set; }
        public List<int>? FeatureIds { get; set; }

        public ICollection<IncludeExteriorTypeVM>? ExteriorTypes { get; set; }
        public List<int>? ExteriorTypeIds { get; set; }

        public ICollection<IncludeParkingTypeVM>? ParkingTypes { get; set; }
        public List<int>? ParkingTypeIds { get; set; }

        public ICollection<IncludeRoofTypeVM>? RoofTypes { get; set; }
        public List<int>? RoofTypeIds { get; set; }

        public ICollection<IncludeViewTypeVM>? ViewTypes { get; set; }
        public List<int>? ViewTypeIds { get; set; }
    }
}
