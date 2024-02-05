using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Estate.Application.Abstractions.Services
{
    public interface ISettingsService
    {
        Task<PaginationVM<ItemSettingsVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemSettingsVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<UpdateSettingsVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateSettingsVM update, ModelStateDictionary model);
    }
}
