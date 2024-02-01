using Estate.Application.ViewModels.Category;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<ICollection<ItemCategoryVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemCategoryVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Category, object>>? orderExpression, int page = 1);
        Task<GetCategoryVM> GetByIdAsync(int id, int take, int page = 1);
        Task<bool> CreateAsync(CreateCategoryVM create, ModelStateDictionary model);
        Task<UpdateCategoryVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateCategoryVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
