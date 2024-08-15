using AutoMapper;
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
    public class ViewTypeService : IViewTypeService
    {
        private readonly IMapper _mapper;
        private readonly IViewTypeRepository _repository;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<AppUser> _userManager;

        public ViewTypeService(IMapper mapper, IViewTypeRepository repository,
            IHttpContextAccessor http, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _http = http;
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(CreateViewTypeVM create, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            if (await _repository.CheckUniqueAsync(x => x.Name.ToLower().Trim() == create.Name.ToLower().Trim()))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }
            ViewType item = _mapper.Map<ViewType>(create);

            await _repository.AddAsync(item);
            await _repository.SaveChangeAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ViewType item = await _getByIdAsync(id);

            _repository.Delete(item);
            await _repository.SaveChangeAsync();
        }

        public async Task<ICollection<ItemViewTypeVM>> GetAllWhereAsync(int take, int page)
        {
            ICollection<ViewType> items = await _repository
                .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemViewTypeVM> vMs = _mapper.Map<ICollection<ItemViewTypeVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemViewTypeVM>> GetAllWhereByOrderAsync(int take, Expression<Func<ViewType, object>>? orderExpression, int page)
        {
            ICollection<ViewType> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemViewTypeVM> vMs = _mapper.Map<ICollection<ItemViewTypeVM>>(items);

            return vMs;
        }
        public async Task<PaginationVM<ItemViewTypeVM>> GetFilteredAsync(string? search, int take, int page, int order, bool isDeleted = false)
        {
            if (page <= 0)
                throw new WrongRequestException("Invalid page number.");
            if (take <= 0)
                throw new WrongRequestException("Invalid take value.");
            if (order <= 0)
                throw new WrongRequestException("Invalid order value.");

            string[] includes = { $"{nameof(ViewType.ProductViewTypes)}" };
            double count = await _repository
                .CountAsync(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true, false);

            ICollection<ViewType> items = new List<ViewType>();

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
                     .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }

            ICollection<ItemViewTypeVM> vMs = _mapper.Map<ICollection<ItemViewTypeVM>>(items);

            PaginationVM<ItemViewTypeVM> pagination = new PaginationVM<ItemViewTypeVM>
            {
                Search = search,
                Order = order,
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / take),
                Items = vMs
            };

            return pagination;
        }

        public async Task<GetViewTypeVM> GetByIdAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(ViewType.ProductViewTypes)}.{nameof(ProductViewType.Product)}.{nameof(Product.ProductImages)}" };
            ViewType item = await _getByIdAsync(id, false, includes);

            GetViewTypeVM get = _mapper.Map<GetViewTypeVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ViewType item = await _getByIdAsync(id);

            item.IsDeleted = false;
            await _repository.SaveChangeAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ViewType item = await _getByIdAsync(id);

            item.IsDeleted = true;
            await _repository.SaveChangeAsync();
        }

        public async Task<UpdateViewTypeVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ViewType item = await _getByIdAsync(id);

            UpdateViewTypeVM update = _mapper.Map<UpdateViewTypeVM>(item);

            return update;
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateViewTypeVM update, ModelStateDictionary model)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ViewType item = await _getByIdAsync(id);

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

        private async Task<ViewType> _getByIdAsync(int id, bool isTracking = true, params string[] includes)
        {
            ViewType viewType = await _repository.GetByIdAsync(id, isTracking, includes);
            if (viewType is null)
                throw new NotFoundException($"Roof-Type not found({id})!");

            return viewType;
        }
    }
}
