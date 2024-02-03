namespace Estate.Application.ViewModels
{
    public record UpdateClientVM(string Comment, string Country, string? Specialty, int CorporateId)
    {
        public ICollection<IncludeCorporateVM>? Corporates { get; set; }
    }
}
