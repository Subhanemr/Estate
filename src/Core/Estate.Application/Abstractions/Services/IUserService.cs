using Estate.Application.ViewModels;

namespace Estate.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<PaginationVM<ItemAppUserVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemAppUserVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetAppUserVM> GetByIdAsync(string id);
        Task<GetAppUserVM> GetByUserNameAsync(string userName);
        Task ReverseSoftDeleteAsync(string id);
        Task SoftDeleteAsync(string id);
        Task DeleteAsync(string id);
        Task IsSoulOfAgencyAsync(string id);
    }
}
