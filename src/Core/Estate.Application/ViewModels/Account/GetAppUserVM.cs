namespace Estate.Application.ViewModels
{
    public record GetAppUserVM(string Id, string Name, string Surname, string UserName, string Img, string Email,  string? PhoneSecond,
        string? Address, string? About, string? FaceLink, string? TwitLink, string? GoogleLink, string? LinkedLink, string? InstaLink, 
        bool SoulOfAgency)
    {
        public string? PhoneNumber { get; init; }

        public int AgencyId { get; set; }
        public IncludeAgencyVM Agency { get; init; }
        public ICollection<IncludeProductVM> Products { get; init; }
        public ICollection<IncludeAppUserImage> Images { get; init; }
    }
}
