using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IViewTypeService
    {
        Task<ICollection<ItemViewTypeVM>> GetAllWhereAsync(int take, int page);
        Task<ICollection<ItemViewTypeVM>> GetAllWhereByOrderAsync(int take, Expression<Func<ViewType, object>>? orderExpression, int page);
        Task<PaginationVM<ItemViewTypeVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemViewTypeVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetViewTypeVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateViewTypeVM create, ModelStateDictionary model);
        Task<UpdateViewTypeVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateViewTypeVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
