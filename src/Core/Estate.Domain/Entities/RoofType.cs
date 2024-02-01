namespace Estate.Domain.Entities
{
    public class RoofType: BaseNameEntity
    {
        public ICollection<ProductRoofType>? ProductRoofTypes { get; set; }
    }
}
