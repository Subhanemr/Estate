using Estate.Domain.Entities;

namespace Estate.Application.ViewModels
{
    public record GetProductCommentVM(int Id, string Comment, DateTime CoomentTime, 
        string AppUserId, AppUser AppUser, int ProductId, IncludeProductVM Product, ICollection<IncludeProductReplyVM> Replies);
}
