namespace Estate.Application.ViewModels
{
    public record ItemAgencyVM(int Id, string Name)
    {
        public ICollection<IncludeAppUserVM> AppUsers { get; set; }
    }
}
