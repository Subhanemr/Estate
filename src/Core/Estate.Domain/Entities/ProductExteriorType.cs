namespace Estate.Domain.Entities
{
    public class ProductExteriorType : BaseNameEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
