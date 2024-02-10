using Estate.Application.ViewModels;

namespace Estate.Application.Abstractions.Services
{
    public interface IFavoriteService
    {
        Task<ICollection<FavoriteItemVM>> WishList();
        Task AddWishList(int id);
        Task DeleteItem(int id);
    }
}
