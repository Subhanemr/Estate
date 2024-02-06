namespace Estate.Application.ViewModels
{
    public record ItemCorporateVM(int Id, string Name, string CorporateLink, string Description, string Img, ICollection<IncludeClientVM>? Clients);
}
