namespace Estate.Application.ViewModels
{
    public record GetCorporateVM(int Id, string Name, string CorporateLink, string Description, string Img, ICollection<IncludeClientVM> Clients);
}
