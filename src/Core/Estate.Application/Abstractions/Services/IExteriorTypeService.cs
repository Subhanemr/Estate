using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IExteriorTypeService
    {
        Task<ICollection<ItemExteriorTypeVM>> GetAllWhereAsync(int take, int page);
        Task<ICollection<ItemExteriorTypeVM>> GetAllWhereByOrderAsync(int take, Expression<Func<ExteriorType, object>>? orderExpression, int page);
        Task<PaginationVM<ItemExteriorTypeVM>> GetFilteredAsync(string? search, int take, int page, int order, bool isDeleted = false);
        Task<GetExteriorTypeVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateExteriorTypeVM create, ModelStateDictionary model);
        Task<UpdateExteriorTypeVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateExteriorTypeVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
