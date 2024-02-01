using Estate.Application.ViewModels.Product;

namespace Estate.Application.ViewModels.ParkingType
{
    public record GetParkingTypeVM(int Id, string Name, ICollection<IncludeProductVM> Products);
}
