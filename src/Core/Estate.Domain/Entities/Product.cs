namespace Estate.Domain.Entities
{
    public class Product : BaseNameEntity
    {
        public double Price { get; set; }
        public float LotSize { get; set; }
        public float Area { get; set; }
        public byte Bedrooms { get; set; }
        public byte Bathrooms { get; set; }
        public byte Garages { get; set; }
        public short YearBuilt { get; set; }
        public byte RoomCount { get; set; }

        public string? SchoolDistrict { get; set; }
        public string? SchoolDistrictPhone { get; set; }
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string FaceLink { get; set; } = null!;
        public string TwitLink { get; set; } = null!;
        public string? GoogleLink { get; set; } = null!;

        public bool HighSchool { get; set; }
        public bool MiddleSchool { get; set; }
        public bool ElementarySchool { get; set; }

        public int CategoryId { get; set; }
        public string AppUserId { get; set; } = null!;
        public Category? Category { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; } = null!;
        public ICollection<ProductComment>? ProductComments { get; set; }
        public ICollection<ProductFeatures>? ProductFeatures { get; set; }
        public ICollection<ProductExteriorType>? ProductExteriorTypes { get; set; }
        public ICollection<ProductParkingType>? ProductParkingTypes { get; set; }
        public ICollection<ProductRoofType>? ProductRoofTypes { get; set; }
        public ICollection<ProductViewType>? ProductViewTypes { get; set; }


    }
}
