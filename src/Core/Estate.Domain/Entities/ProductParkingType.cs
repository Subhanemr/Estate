namespace Estate.Domain.Entities
{
    public class ProductParkingType : BaseNameEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
