namespace Estate.Domain.Entities
{
    public class ProductFeatures : BaseEntity
    {
        public int ProductId { get; set; }
        public int FeaturesId { get; set; }
        public Product Product { get; set; } = null!;
        public Features Features { get; set; } = null!;
    }
}
