using Estate.Application.ViewModels.ParkingType;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IParkingTypeService
    {
        Task<ICollection<ItemParkingTypeVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemParkingTypeVM>> GetAllWhereByOrderAsync(int take, Expression<Func<ParkingType, object>>? orderExpression, int page = 1);
        Task<GetParkingTypeVM> GetByIdAsync(int id, int take, int page = 1);
        Task<bool> CreateAsync(CreateParkingTypeVM create, ModelStateDictionary model);
        Task<UpdateParkingTypeVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateParkingTypeVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
