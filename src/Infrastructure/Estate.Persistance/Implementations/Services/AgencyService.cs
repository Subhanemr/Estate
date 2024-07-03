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
    public class AgencyService : IAgencyService
    {
        private readonly IMapper _mapper;
        private readonly IAgencyRepository _repository;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<AppUser> _userManager;

        public AgencyService(IMapper mapper, IAgencyRepository repository,
            IHttpContextAccessor http, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _http = http;
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(CreateAgencyVM create, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            if (await _repository.CheckUniqueAsync(x => x.Name.ToLower().Trim() == create.Name.ToLower().Trim()))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }
            Agency item = _mapper.Map<Agency>(create);

            await _repository.AddAsync(item);
            await _repository.SaveChangeAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Agency item = await _getByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            _repository.Delete(item);
            await _repository.SaveChangeAsync();
        }


        public async Task<ICollection<ItemAgencyVM>> GetAllAsync()
        {
            ICollection<Agency> items = await _repository
                .GetAll(false).ToListAsync();

            ICollection<ItemAgencyVM> vMs = _mapper.Map<ICollection<ItemAgencyVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemAgencyVM>> GetAllWhereAsync(int take, int page)
        {
            ICollection<Agency> items = await _repository
                .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemAgencyVM> vMs = _mapper.Map<ICollection<ItemAgencyVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemAgencyVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Agency, object>>? orderExpression, int page)
        {
            ICollection<Agency> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemAgencyVM> vMs = _mapper.Map<ICollection<ItemAgencyVM>>(items);

            return vMs;
        }

        public async Task<PaginationVM<ItemAgencyVM>> GetFilteredAsync(string? search, int take, int page, int order, bool isDeleted = false)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes = { $"{nameof(Agency.AppUsers)}" };
            double count = await _repository
                .CountAsync(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true, false);

            ICollection<Agency> items = new List<Agency>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, false, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, true, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }
            ICollection<ItemAgencyVM> vMs = _mapper.Map<ICollection<ItemAgencyVM>>(items);

            PaginationVM<ItemAgencyVM> pagination = new PaginationVM<ItemAgencyVM>
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

        public async Task<GetAgencyVM> GetByIdAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Agency.AppUsers)}.{nameof(AppUser.AppUserImages)}" };
            Agency item = await _getByIdAsync(id, false, includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            GetAgencyVM get = _mapper.Map<GetAgencyVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Agency item = await _getByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = false;
            await _repository.SaveChangeAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Agency item = await _getByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChangeAsync();
        }

        public async Task<UpdateAgencyVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Agency item = await _getByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdateAgencyVM update = _mapper.Map<UpdateAgencyVM>(item);

            return update;
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateAgencyVM update, ModelStateDictionary model)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Agency item = await _getByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

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

        private async Task<Agency> _getByIdAsync(int id, bool isTracking = true, params string[] includes)
        {
            Agency agency = await _repository.GetByIdAsync(id, isTracking, includes);
            if (agency is null)
                throw new NotFoundException($"Agency not found({id})!");

            return agency;
        }
    }
}
