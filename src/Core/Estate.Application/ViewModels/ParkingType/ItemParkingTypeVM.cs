namespace Estate.Application.ViewModels
{
    public record ItemParkingTypeVM(int Id, string Name)
    {
        public ICollection<IncludeProductVM>? Products { get; init; }

    }
}
