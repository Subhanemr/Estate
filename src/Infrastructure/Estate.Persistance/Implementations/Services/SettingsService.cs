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

namespace Estate.Persistance.Implementations.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IMapper _mapper;
        private readonly ISettingsRepository _repository;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<AppUser> _userManager;

        public SettingsService(ISettingsRepository repository, IHttpContextAccessor http, UserManager<AppUser> userManager, IMapper mapper)
        {
            _repository = repository;
            _http = http;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<PaginationVM<ItemSettingsVM>> GetFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            double count = await _repository
                .CountAsync(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Key.ToLower().Contains(search.ToLower()) : true, false);

            ICollection<Settings> items = new List<Settings>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Key.ToLower().Contains(search.ToLower()) : true,
                        x => x.Key, false, false, (page - 1) * take, take, false).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(expression: x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Key.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, false, (page - 1) * take, take, false).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Key.ToLower().Contains(search.ToLower()) : true,
                        x => x.Key, true, false, (page - 1) * take, take, false).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Key.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, false, (page - 1) * take, take, false).ToListAsync();
                    break;
            }

            ICollection<ItemSettingsVM> vMs = _mapper.Map<ICollection<ItemSettingsVM>>(items);

            PaginationVM<ItemSettingsVM> pagination = new PaginationVM<ItemSettingsVM>
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

        public async Task<UpdateSettingsVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Settings item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdateSettingsVM update = new UpdateSettingsVM { Key = item.Key, Value = item.Value };

            return update;
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateSettingsVM update, ModelStateDictionary model)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Settings item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            if (await _repository.CheckUniqueAsync(x => x.Key.ToLower().Trim() == update.Key.ToLower().Trim() && x.Id != id))
            {
                model.AddModelError("Key", "Key is exists");
                return false;
            }
            item.Value = update.Value;
            _repository.Update(item);
            await _repository.SaveChangeAsync();
            return true;
        }
    }
}
