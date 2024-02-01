using Estate.Application.ViewModels.Product;

namespace Estate.Application.ViewModels.ParkingType
{
    public record ItemParkingTypeVM(int Id, string Name, ICollection<IncludeProductVM>? Products);
}
