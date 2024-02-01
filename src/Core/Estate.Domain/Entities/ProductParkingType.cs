namespace Estate.Domain.Entities
{
    public class ProductParkingType : BaseEntity
    {
        public int ProductId { get; set; }
        public int ParkingTypeId { get; set; }
        public Product? Product { get; set; } 
        public ParkingType? ParkingType { get; set; } 
    }
}
