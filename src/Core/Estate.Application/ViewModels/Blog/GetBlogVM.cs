using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record GetBlogVM(int Id, string Name, string Description, DateTime CreateAt, string CreatedBy,
        string FaceLink, string TwitLink, string? GoogleLink, string LinkedLink, string InstaLink)
    {
        public ICollection<IncludeBlogCommnetVM>? Commnets { get; init; }
        public ICollection<IncludeBlogImageVM> Images { get; init; }
    }
}
