using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IExteriorTypeService
    {
        Task<ICollection<ItemExteriorTypeVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemExteriorTypeVM>> GetAllWhereByOrderAsync(int take, Expression<Func<ExteriorType, object>>? orderExpression, int page = 1);
        Task<GetExteriorTypeVM> GetByIdAsync(int id, int take, int page = 1);
        Task<bool> CreateAsync(CreateExteriorTypeVM create, ModelStateDictionary model);
        Task<UpdateExteriorTypeVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateExteriorTypeVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
