using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface ICorporateService
    {
        Task<ICollection<ItemCorporateVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemCorporateVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Corporate, object>>? orderExpression, int page = 1);
        Task<GetCorporateVM> GetByIdAsync(int id, int take, int page = 1);
        Task<bool> CreateAsync(CreateCorporateVM create, ModelStateDictionary model);
        Task<UpdateCorporateVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateCorporateVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
