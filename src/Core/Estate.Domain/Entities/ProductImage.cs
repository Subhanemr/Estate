namespace Estate.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public string Url { get; set; } = null!;
        public bool? IsPrimary { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; } 
    }
}
