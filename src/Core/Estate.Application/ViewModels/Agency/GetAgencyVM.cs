namespace Estate.Application.ViewModels
{
    public record GetAgencyVM
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public ICollection<IncludeAppUserVM> AppUsers { get; set; }
    }
}
