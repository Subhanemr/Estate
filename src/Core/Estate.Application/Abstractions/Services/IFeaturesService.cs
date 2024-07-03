using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IFeaturesService
    {
        Task<ICollection<ItemFeaturesVM>> GetAllWhereAsync(int take, int page);
        Task<ICollection<ItemFeaturesVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Features, object>>? orderExpression, int page);
        Task<PaginationVM<ItemFeaturesVM>> GetFilteredAsync(string? search, int take, int page, int order, bool isDeleted = false);
        Task<GetFeaturesVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateFeaturesVM create, ModelStateDictionary model);
        Task<UpdateFeaturesVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateFeaturesVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
