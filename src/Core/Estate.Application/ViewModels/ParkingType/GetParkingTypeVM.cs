namespace Estate.Application.ViewModels
{
    public record GetParkingTypeVM(int Id, string Name, ICollection<IncludeProductVM> Products);
}
