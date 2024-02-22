using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record GetAppUserVM
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string UserName { get; init; }
        public string Img { get; init; }
        public string Email { get; init; }
        public string? PhoneSecond { get; init; }
        public string? Address { get; init; }
        public string? About { get; init; }
        public string? FaceLink { get; init; }
        public string? TwitLink { get; init; }
        public string? GoogleLink { get; init; }
        public string? LinkedLink { get; init; }
        public string? InstaLink { get; init; }
        public bool SoulOfAgency { get; init; }

        public string? PhoneNumber { get; init; }

        public int AgencyId { get; set; }
        public IncludeAgencyVM Agency { get; init; }
        public ICollection<IncludeProductVM>? Favorites { get; init; }
        public ICollection<IncludeProductVM> Products { get; init; }
        public ICollection<IncludeAppUserImage> Images { get; init; }
    }
}
