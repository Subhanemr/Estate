namespace Estate.Domain.Entities
{
    public class ExteriorType : BaseNameEntity
    {
        public ICollection<ProductExteriorType>? ProductExteriorTypes { get; set; }
    }
}
