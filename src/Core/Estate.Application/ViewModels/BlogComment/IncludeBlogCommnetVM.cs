using Estate.Application.ViewModels.BlogReply;
using Estate.Domain.Entities;

namespace Estate.Application.ViewModels.BlogComment
{
    public record IncludeBlogCommnetVM(int Id, string Comment, DateTime CoomentTime, 
        string AppUserId, AppUser? AppUser, int BlogId, ICollection<IncludeBlogReply> Replies);
}

