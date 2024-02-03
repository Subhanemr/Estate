namespace Estate.Application.ViewModels
{
    public record CreateClientVM(string Comment, string Country, string? Specialty, int CorporateId)
    {
        public ICollection<IncludeCorporateVM>? Corporates { get; set; }
    }
}
