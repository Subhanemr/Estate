using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IAgencyService
    {
        Task<ICollection<ItemAgencyVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemAgencyVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Agency, object>>? orderExpression, int page = 1);
        Task<PaginationVM<ItemAgencyVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemAgencyVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetAgencyVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateAgencyVM create, ModelStateDictionary model);
        Task<UpdateAgencyVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateAgencyVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
