using Estate.Application.ViewModels.Account;
using Estate.Domain.Entities;

namespace Estate.Application.ViewModels.Agency
{
    public record GetAgencyVM(int Id, string Name, ICollection<IncludeAppUser> AppUsers);
}
