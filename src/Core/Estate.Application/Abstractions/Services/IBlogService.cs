using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IBlogService
    {
        Task<ICollection<ItemBlogVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemBlogVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Blog, object>>? orderExpression, int page = 1);
        Task<GetBlogVM> GetByIdAsync(int id, int take, int page = 1);
        Task<bool> CreateAsync(CreateBlogVM create, ModelStateDictionary model);
        Task<UpdateBlogVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateBlogVM update, ModelStateDictionary model);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
    }
}
