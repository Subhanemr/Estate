﻿using Estate.Application.ViewModels.Category;
using Estate.Application.ViewModels.ProductImage;

namespace Estate.Application.ViewModels.Product
{
    public record ItemProductVM(int Id, string Name, double Price, float LotSize, float Area, byte Bedrooms, byte Bathrooms,
        byte Garages, short YearBuilt, byte RoomCount, string? SchoolDistrict, string? SchoolDistrictPhone,
        string Address, string Description, string FaceLink, string TwitLink, string? GoogleLink,
        bool HighSchool, bool MiddleSchool, bool ElementarySchool, int CategoryId, IncludeCategoryVM Category,
        ICollection<IncludeProductImageVM> Images);
}
