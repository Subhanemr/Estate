using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<ICollection<ItemActorVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemActorVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Product, object>>? orderExpression, int page = 1);
        Task<GetActorVM> GetByIdAsync(int id, int take, int page = 1);
        Task<bool> CreateAsync(string? search, CreateActorVM create, ModelStateDictionary model);
        Task<UpdateActorVM> UpdateAsync(int id, string? search);
        Task<bool> UpdatePostAsync(int id, UpdateActorVM update, ModelStateDictionary model, string? search);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
        void CreatePopulateDropdowns(CreateActorVM create, string? search);
        void UpdatePopulateDropdowns(UpdateActorVM update, string? search);
    }
}
