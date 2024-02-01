using Estate.Application.ViewModels.Product;
using Estate.Domain.Entities;

namespace Estate.Application.ViewModels.ProductComment
{
    public record IncludeProductCommentVM(int Id, string Comment, DateTime CoomentTime,
        string AppUserId, AppUser? AppUser, int ProductId, IncludeProductVM? Product);
}
