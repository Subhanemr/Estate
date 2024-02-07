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
            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);
            item.CreatedBy = user.UserName;

            await _repository.AddAsync(item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ViewType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            _repository.Delete(item);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemViewTypeVM>> GetAllWhereAsync(int take, int page = 1)
        {
            ICollection<ViewType> items = await _repository
                .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemViewTypeVM> vMs = _mapper.Map<ICollection<ItemViewTypeVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemViewTypeVM>> GetAllWhereByOrderAsync(int take, Expression<Func<ViewType, object>>? orderExpression, int page = 1)
        {
            ICollection<ViewType> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemViewTypeVM> vMs = _mapper.Map<ICollection<ItemViewTypeVM>>(items);

            return vMs;
        }
        public async Task<PaginationVM<ItemViewTypeVM>> GetFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes = { $"{nameof(ViewType.ProductViewTypes)}" };
            double count = await _repository.CountAsync();

            ICollection<ViewType> items = new List<ViewType>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, false, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, true, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, false, (page - 1) * take, take, false, includes).ToListAsync();
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
        public async Task<PaginationVM<ItemViewTypeVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes = { $"{nameof(ViewType.ProductViewTypes)}" };
            double count = await _repository.CountAsync();

            ICollection<ViewType> items = new List<ViewType>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, false, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, true, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }

            ICollection<ItemViewTypeVM> vMs = _mapper.Map<ICollection<ItemViewTypeVM>>(items);

            PaginationVM<ItemViewTypeVM> pagination = new PaginationVM<ItemViewTypeVM>
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
        public async Task<GetViewTypeVM> GetByIdAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(ViewType.ProductViewTypes)}" };
            ViewType item = await _repository.GetByIdAsync(id, IsTracking: false, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            GetViewTypeVM get = _mapper.Map<GetViewTypeVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ViewType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = false;
            await _repository.SaveChanceAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ViewType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChanceAsync();
        }

        public async Task<UpdateViewTypeVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ViewType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdateViewTypeVM update = _mapper.Map<UpdateViewTypeVM>(item);

            return update;
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateViewTypeVM update, ModelStateDictionary model)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ViewType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            if (await _repository.CheckUniqueAsync(x => x.Name.ToLower().Trim() == update.Name.ToLower().Trim() && x.Id != id))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }
            _mapper.Map(update, item);
            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);
            item.CreatedBy = user.UserName;
            _repository.Update(item);
            await _repository.SaveChanceAsync();
            return true;
        }
    }
}
