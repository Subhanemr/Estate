namespace Estate.Domain.Entities
{
    public class Features : BaseNameEntity
    {
        public ICollection<ProductFeatures>? ProductFeatures { get; set; }
    }
}
