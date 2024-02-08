namespace Estate.Application.Abstractions.Services
{
    public interface IFavoriteService
    {
        Task AddWishList(int id);
        Task DeleteItem(int id);
    }
}
