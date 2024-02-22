namespace Estate.Application.ViewModels
{
    public record GetExteriorTypeVM
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public ICollection<IncludeProductVM> Products { get; init; }
    }
}
