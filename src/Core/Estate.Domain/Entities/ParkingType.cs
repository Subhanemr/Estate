namespace Estate.Domain.Entities
{
    public class ParkingType : BaseNameEntity
    {
        public ICollection<ProductParkingType>? ProductParkingTypes { get; set; }
    }
}
