namespace Estate.Application.ViewModels
{
    public record ItemCorporateVM(int Id, string CorporateLink, string Description, string Img, ICollection<IncludeClientVM>? Clients);
}
