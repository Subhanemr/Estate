using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IViewTypeService
    {
        Task<ICollection<ItemViewTypeVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemViewTypeVM>> GetAllWhereByOrderAsync(int take, Expression<Func<ViewType, object>>? orderExpression, int page = 1);
        Task<GetViewTypeVM> GetByIdAsync(int id, int take, int page = 1);
        Task<bool> CreateAsync(CreateViewTypeVM create, ModelStateDictionary model);
        Task<UpdateViewTypeVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateViewTypeVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
