namespace Estate.Domain.Entities
{
    public class ProductExteriorType : BaseEntity
    {
        public int ProductId { get; set; }
        public int ExteriorTypeId { get; set; }
        public Product? Product { get; set; } 
        public ExteriorType? ExteriorType { get; set; }
    }
}
