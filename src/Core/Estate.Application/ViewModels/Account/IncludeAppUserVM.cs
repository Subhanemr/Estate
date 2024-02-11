namespace Estate.Application.ViewModels
{
    public record IncludeAppUserVM(string Id, string Name, string Surname, string UserName, string Img, string PhoneNumber,
        string? PhoneSecond, string? Address, string? About, string? FaceLink, string? TwitLink, string? GoogleLink, string? LinkedLink, string? InstaLink)
    {
        public IncludeAgencyVM? AgencyUser { get; init; }
    }
}
