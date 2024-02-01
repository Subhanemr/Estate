namespace Estate.Domain.Entities
{
    public class ProductFeatures : BaseEntity
    {
        public int ProductId { get; set; }
        public int FeaturesId { get; set; }
        public Product? Product { get; set; } 
        public Features? Features { get; set; } 
    }
}
