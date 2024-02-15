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
    public class RoofTypeService : IRoofTypeService
    {
        private readonly IMapper _mapper;
        private readonly IRoofTypeRepository _repository;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<AppUser> _userManager;

        public RoofTypeService(IMapper mapper, IRoofTypeRepository repository,
            IHttpContextAccessor http, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _http = http;
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(CreateRoofTypeVM create, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            if (await _repository.CheckUniqueAsync(x => x.Name.ToLower().Trim() == create.Name.ToLower().Trim()))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }
            RoofType item = _mapper.Map<RoofType>(create);
            //item.CreatedBy = user.UserName;

            await _repository.AddAsync(item);
            await _repository.SaveChangeAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            RoofType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            _repository.Delete(item);
            await _repository.SaveChangeAsync();
        }

        public async Task<ICollection<ItemRoofTypeVM>> GetAllWhereAsync(int take, int page = 1)
        {
            ICollection<RoofType> items = await _repository
                .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemRoofTypeVM> vMs = _mapper.Map<ICollection<ItemRoofTypeVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemRoofTypeVM>> GetAllWhereByOrderAsync(int take, Expression<Func<RoofType, object>>? orderExpression, int page = 1)
        {
            ICollection<RoofType> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemRoofTypeVM> vMs = _mapper.Map<ICollection<ItemRoofTypeVM>>(items);

            return vMs;
        }
        public async Task<PaginationVM<ItemRoofTypeVM>> GetFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes = { $"{nameof(RoofType.ProductRoofTypes)}" };
            double count = await _repository.CountAsync();

            ICollection<RoofType> items = new List<RoofType>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, false, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, true, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }

            ICollection<ItemRoofTypeVM> vMs = _mapper.Map<ICollection<ItemRoofTypeVM>>(items);

            PaginationVM<ItemRoofTypeVM> pagination = new PaginationVM<ItemRoofTypeVM>
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
        public async Task<PaginationVM<ItemRoofTypeVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes = { $"{nameof(RoofType.ProductRoofTypes)}" };
            double count = await _repository.CountAsync();

            ICollection<RoofType> items = new List<RoofType>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, false, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, true, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }

            ICollection<ItemRoofTypeVM> vMs = _mapper.Map<ICollection<ItemRoofTypeVM>>(items);

            PaginationVM<ItemRoofTypeVM> pagination = new PaginationVM<ItemRoofTypeVM>
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
        public async Task<GetRoofTypeVM> GetByIdAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(RoofType.ProductRoofTypes)}.{nameof(ProductRoofType.Product)}.{nameof(Product.ProductImages)}" };
            RoofType item = await _repository.GetByIdAsync(id, IsTracking: false, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            GetRoofTypeVM get = _mapper.Map<GetRoofTypeVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            RoofType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = false;
            await _repository.SaveChangeAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            RoofType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChangeAsync();
        }

        public async Task<UpdateRoofTypeVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            RoofType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdateRoofTypeVM update = _mapper.Map<UpdateRoofTypeVM>(item);

            return update;
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateRoofTypeVM update, ModelStateDictionary model)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            RoofType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            if (await _repository.CheckUniqueAsync(x => x.Name.ToLower().Trim() == update.Name.ToLower().Trim() && x.Id != id))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }
            _mapper.Map(update, item);
            //item.CreatedBy = user.UserName;
            _repository.Update(item);
            await _repository.SaveChangeAsync();
            return true;
        }
    }
}
