namespace Estate.Application.ViewModels
{
    public record GetAgencyVM(int Id, string Name, ICollection<IncludeAppUser> AppUsers);
}
