using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface ICorporateService
    {
        Task<ICollection<ItemCorporateVM>> GetAllWhereAsync(int take, int page);
        Task<ICollection<ItemCorporateVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Corporate, object>>? orderExpression, int page);
        Task<PaginationVM<ItemCorporateVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemCorporateVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetCorporateVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateCorporateVM create, ModelStateDictionary model);
        Task<UpdateCorporateVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateCorporateVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
