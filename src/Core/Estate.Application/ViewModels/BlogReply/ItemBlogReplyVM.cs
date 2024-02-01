using Estate.Domain.Entities;

namespace Estate.Application.ViewModels.BlogReply
{
    public record ItemBlogReplyVM(int Id, string Comment, DateTime CoomentTime, 
        string AppUserId, AppUser AppUser, int BlogCommnetId);
}
