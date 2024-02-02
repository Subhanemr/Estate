using Estate.Application.ViewModels.Category;
using Estate.Application.ViewModels.ExteriorType;
using Estate.Application.ViewModels.Features;
using Estate.Application.ViewModels.ParkingType;
using Estate.Application.ViewModels.ProductComment;
using Estate.Application.ViewModels.ProductImage;
using Estate.Application.ViewModels.RoofType;
using Estate.Application.ViewModels.ViewType;

namespace Estate.Application.ViewModels.Product
{
    public record GetProductVM(int Id, string Name, double Price, float LotSize, float Area, byte Bedrooms, byte Bathrooms,
        byte Garages, short YearBuilt, byte RoomCount, string? SchoolDistrict, string? SchoolDistrictPhone,
        string Address, string Description, string FaceLink, string TwitLink, string? GoogleLink,
        bool HighSchool, bool MiddleSchool, bool ElementarySchool, int CategoryId, IncludeCategoryVM Category,
        ICollection<IncludeProductImageVM> Images, ICollection<IncludeProductCommentVM>? Comments,
        ICollection<IncludeFeaturesVM>? Features, ICollection<IncludeExteriorTypeVM>? ExteriorTypes,
        ICollection<IncludeParkingTypeVM>? ParkingTypes, ICollection<IncludeRoofTypeVM>? RoofTypes,
        ICollection<IncludeViewTypeVM>? ViewTypes);
}
