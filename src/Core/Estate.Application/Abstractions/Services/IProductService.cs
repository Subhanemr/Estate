using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<ICollection<ItemProductVM>> GetAllWhereAsync(int take, int page);
        Task<ICollection<ItemProductVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Product, object>>? orderExpression, int page);
        Task<ICollection<ItemProductVM>> GetAllWhereByBoolAsync(int take, Expression<Func<Product, bool>>? expression, int page);
        Task<PaginationVM<ProductFilterVM>> GetFilteredAsync(string? search, int take, int page, int order,
            int? categoryId, int? minPrice, int? maxPrice, int? minArea, int? maxArea, int? minBeds, int? minBaths, bool isDeleted = false);
        Task<PaginationVM<ProductFilterVM>> GetAllWhereByOrderFilterAsync(int take, int page, Expression<Func<Product, object>>? orderExpression);
        Task<GetProductVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateProductVM create, ModelStateDictionary model, ITempDataDictionary tempData);
        Task<UpdateProductVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateProductVM update, ModelStateDictionary model, ITempDataDictionary tempData);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
        Task CreatePopulateDropdowns(CreateProductVM create);
        Task UpdatePopulateDropdowns(UpdateProductVM update);
        Task<bool> CommentAsync(int productId, string comment, ITempDataDictionary tempData);
        Task<bool> ReplyAsync(int productCommnetId, string comment, ITempDataDictionary tempData);
        Task<bool> AgentMessage(int productId, string message, ITempDataDictionary tempData);
    }
}
