namespace Estate.Application.ViewModels
{
    public record ItemAgencyVM(int Id, string Name, ICollection<IncludeAppUserVM> AppUsers);
}
