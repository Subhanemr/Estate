namespace Estate.Application.ViewModels
{
    public record GetCorporateVM(int Id,string CorporateLink, string Description, string Img, ICollection<IncludeClientVM> Clients);
}
