namespace Estate.Application.ViewModels
{
    public record GetAgencyVM(int Id, string Name)
    {
        public ICollection<IncludeAppUserVM> AppUsers { get; set; }
    }
}
