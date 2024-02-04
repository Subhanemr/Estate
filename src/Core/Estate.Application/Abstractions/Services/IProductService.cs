﻿using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq.Expressions;

namespace Estate.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<ICollection<ItemProductVM>> GetAllWhereAsync(int take, int page = 1);
        Task<ICollection<ItemProductVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Product, object>>? orderExpression, int page = 1);
        Task<PaginationVM<ItemProductVM>> GetFilteredAsync(string? search, int take, int page, int order, int? categoryId);
        Task<PaginationVM<ItemProductVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order, int? categoryId);
        Task<GetProductVM> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateProductVM create, ModelStateDictionary model, ITempDataDictionary tempData);
        Task<UpdateProductVM> UpdateAsync(int id);
        Task<bool> UpdatePostAsync(int id, UpdateProductVM update, ModelStateDictionary model, ITempDataDictionary tempData);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
        void CreatePopulateDropdowns(CreateProductVM create);
        void UpdatePopulateDropdowns(UpdateProductVM update);
    }
}
