using Estate.Application.ViewModels.Client;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IClientService
    {
        Task<ICollection<ItemClientVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemClientVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Client, object>>? orderExpression, int page = 1);
        Task<GetClientVM> GetByIdAsync(int id, int take, int page = 1);
        Task<bool> CreateAsync(CreateClientVM create, ModelStateDictionary model);
        Task<UpdateClientVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateClientVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
        void CreatePopulateDropdowns(CreateClientVM create);
        void UpdatePopulateDropdowns(CreateClientVM update);
    }
}
