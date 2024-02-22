namespace Estate.Application.ViewModels
{
    public record ItemAgencyVM
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public ICollection<IncludeAppUserVM> AppUsers { get; set; }
    }
}
