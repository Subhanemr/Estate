using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record IncludeBlogCommnetVM(int Id, string Comment, DateTime CoomentTime, 
        string AppUserId, AppUser? AppUser, int BlogId, ICollection<IncludeBlogReply> Replies);
}

