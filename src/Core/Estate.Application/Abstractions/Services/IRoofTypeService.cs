using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IRoofTypeService
    {
        Task<ICollection<ItemRoofTypeVM>> GetAllWhereAsync(int take, int page);
        Task<ICollection<ItemRoofTypeVM>> GetAllWhereByOrderAsync(int take, Expression<Func<RoofType, object>>? orderExpression, int page);
        Task<PaginationVM<ItemRoofTypeVM>> GetFilteredAsync(string? search, int take, int page, int order, bool isDeleted = false);
        Task<GetRoofTypeVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateRoofTypeVM create, ModelStateDictionary model);
        Task<UpdateRoofTypeVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateRoofTypeVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
