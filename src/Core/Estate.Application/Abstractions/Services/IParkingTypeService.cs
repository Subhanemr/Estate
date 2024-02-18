using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IParkingTypeService
    {
        Task<ICollection<ItemParkingTypeVM>> GetAllWhereAsync(int take, int page);
        Task<ICollection<ItemParkingTypeVM>> GetAllWhereByOrderAsync(int take, Expression<Func<ParkingType, object>>? orderExpression, int page);
        Task<PaginationVM<ItemParkingTypeVM>> GetFilteredAsync(string? search, int take, int page, int order);
        Task<PaginationVM<ItemParkingTypeVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order);
        Task<GetParkingTypeVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateParkingTypeVM create, ModelStateDictionary model);
        Task<UpdateParkingTypeVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateParkingTypeVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
