namespace Estate.Application.ViewModels
{
    public record ItemParkingTypeVM(int Id, string Name, ICollection<IncludeProductVM>? Products);
}
