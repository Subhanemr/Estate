namespace Estate.Domain.Entities
{
    public class ProductParkingType : BaseEntity
    {
        public int ProductId { get; set; }
        public int ParkingTypeId { get; set; }
        public Product Product { get; set; } = null!;
        public ParkingType ParkingType { get; set; } = null!;
    }
}
