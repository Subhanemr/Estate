using Estate.Application.ViewModels.Client;
using Microsoft.AspNetCore.Http;

namespace Estate.Application.ViewModels.Corporate
{
    public record GetCorporateVM(int Id,string CorporateLink, string Description, string Img, ICollection<IncludeClientVM> Clients);
}
