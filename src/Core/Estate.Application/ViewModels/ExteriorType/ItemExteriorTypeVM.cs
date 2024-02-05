namespace Estate.Application.ViewModels
{
    public record ItemExteriorTypeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<IncludeProductVM> Products { get; init; }


    }
}
