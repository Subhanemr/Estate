using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IClientService
    {
        Task<ICollection<ItemClientVM>> GetAllWhereAsync(int take, int page);
        Task<ICollection<ItemClientVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Client, object>>? orderExpression, int page);
        Task<PaginationVM<ItemClientVM>> GetFilteredAsync(string? search, int take, int page, int order, bool isDeleted = false);
        Task<GetClientVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateClientVM create, ModelStateDictionary model);
        Task<UpdateClientVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateClientVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
        Task CreatePopulateDropdowns(CreateClientVM create);
        Task UpdatePopulateDropdowns(UpdateClientVM update);
    }
}
