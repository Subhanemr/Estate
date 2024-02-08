namespace Estate.Domain.Entities
{
    public class ProductRoofType : BaseEntity
    {
        public int ProductId { get; set; }
        public int RoofTypeId { get; set; }
        public Product? Product { get; set; } 
        public RoofType? RoofType { get; set; } 
    }
}
