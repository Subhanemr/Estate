﻿using AutoMapper;
using Estate.Application.Abstractions.Repositories;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Estate.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Estate.Persistance.Implementations.Services
{
    public class ExteriorTypeService : IExteriorTypeService
    {
        private readonly IMapper _mapper;
        private readonly IExteriorTypeRepository _repository;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<AppUser> _userManager;

        public ExteriorTypeService(IMapper mapper, IExteriorTypeRepository repository,
            IHttpContextAccessor http, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _http = http;
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(CreateExteriorTypeVM create, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            if (await _repository.CheckUniqueAsync(x => x.Name.ToLower().Trim() == create.Name.ToLower().Trim()))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }
            ExteriorType item = _mapper.Map<ExteriorType>(create);

            await _repository.AddAsync(item);
            await _repository.SaveChangeAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ExteriorType item = await _getByIdAsync(id);

            _repository.Delete(item);
            await _repository.SaveChangeAsync();
        }

        public async Task<ICollection<ItemExteriorTypeVM>> GetAllWhereAsync(int take, int page)
        {
            ICollection<ExteriorType> items = await _repository
                .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemExteriorTypeVM> vMs = _mapper.Map<ICollection<ItemExteriorTypeVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemExteriorTypeVM>> GetAllWhereByOrderAsync(int take, Expression<Func<ExteriorType, object>>? orderExpression, int page)
        {
            ICollection<ExteriorType> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemExteriorTypeVM> vMs = _mapper.Map<ICollection<ItemExteriorTypeVM>>(items);

            return vMs;
        }
        public async Task<PaginationVM<ItemExteriorTypeVM>> GetFilteredAsync(string? search, int take, int page, int order, bool isDeleted = false)
        {
            if (page <= 0)
                throw new WrongRequestException("Invalid page number.");
            if (take <= 0)
                throw new WrongRequestException("Invalid take value.");
            if (order <= 0)
                throw new WrongRequestException("Invalid order value.");

            string[] includes = { $"{nameof(ExteriorType.ProductExteriorTypes)}.{nameof(ProductExteriorType.Product)}" };
            double count = await _repository
                .CountAsync(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true, false);

            ICollection<ExteriorType> items = new List<ExteriorType>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, false, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, true, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(expression: x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                     orderException: x => x.CreateAt, true, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }

            ICollection<ItemExteriorTypeVM> vMs = _mapper.Map<ICollection<ItemExteriorTypeVM>>(items);

            PaginationVM<ItemExteriorTypeVM> pagination = new PaginationVM<ItemExteriorTypeVM>
            {
                Take = take,
                Search = search,
                Order = order,
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / take),
                Items = vMs
            };

            return pagination;
        }

        public async Task<GetExteriorTypeVM> GetByIdAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(ExteriorType.ProductExteriorTypes)}.{nameof(ProductExteriorType.Product)}.{nameof(Product.ProductImages)}" };
            ExteriorType item = await _getByIdAsync(id, false, includes);

            GetExteriorTypeVM get = _mapper.Map<GetExteriorTypeVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ExteriorType item = await _getByIdAsync(id);

            item.IsDeleted = false;
            await _repository.SaveChangeAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ExteriorType item = await _getByIdAsync(id);

            item.IsDeleted = true;
            await _repository.SaveChangeAsync();
        }

        public async Task<UpdateExteriorTypeVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ExteriorType item = await _getByIdAsync(id);

            UpdateExteriorTypeVM update = _mapper.Map<UpdateExteriorTypeVM>(item);

            return update;
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateExteriorTypeVM update, ModelStateDictionary model)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ExteriorType item = await _getByIdAsync(id);

            if (await _repository.CheckUniqueAsync(x => x.Name.ToLower().Trim() == update.Name.ToLower().Trim() && x.Id != id))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }
            _mapper.Map(update, item);
            _repository.Update(item);
            await _repository.SaveChangeAsync();
            return true;
        }

        private async Task<ExteriorType> _getByIdAsync(int id, bool isTracking = true, params string[] includes)
        {
            ExteriorType exteriorType = await _repository.GetByIdAsync(id, isTracking, includes);
            if (exteriorType is null)
                throw new NotFoundException($"ExteriorType not found({id})!");

            return exteriorType;
        }
    }
}
