using Estate.Application.ViewModels.BlogComment;
using Estate.Domain.Entities;

namespace Estate.Application.ViewModels.BlogReply
{
    public record IncludeBlogReply(int Id, string Comment, DateTime CoomentTime, 
        string AppUserId, AppUser? AppUser, int BlogCommnetId, IncludeBlogCommnetVM? BlogCommnet);
}
