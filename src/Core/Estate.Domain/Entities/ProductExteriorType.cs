namespace Estate.Domain.Entities
{
    public class ProductExteriorType : BaseEntity
    {
        public int ProductId { get; set; }
        public int ExteriorTypeId { get; set; }
        public Product Product { get; set; } = null!;
        public ExteriorType ExteriorType { get; set; } = null!;
    }
}
