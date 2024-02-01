namespace Estate.Domain.Entities
{
    public class ProductRoofType : BaseNameEntity
    {
        public int ProductId { get; set; }
        public int RoofTypeId { get; set; }
        public Product Product { get; set; } = null!;
        public RoofType RoofType { get; set; } = null!;
    }
}
