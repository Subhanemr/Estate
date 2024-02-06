namespace Estate.Application.ViewModels
{
    public record IncludeBlogReply(int Id, string Comment, DateTime CreateAt, string CreatedBy,
        string AppUserId, IncludeAppUserVM? AppUser, int BlogCommnetId);
}
