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
    public class ParkingTypeService : IParkingTypeService
    {
        private readonly IMapper _mapper;
        private readonly IParkingTypeRepository _repository;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<AppUser> _userManager;

        public ParkingTypeService(IMapper mapper, IParkingTypeRepository repository,  IHttpContextAccessor http, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _http = http;
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(CreateParkingTypeVM create, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            if (await _repository.CheckUniqueAsync(x => x.Name == create.Name))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }
            ParkingType item = _mapper.Map<ParkingType>(create);
            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);
            item.CreatedBy = user.UserName;

            await _repository.AddAsync(item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ParkingType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            _repository.Delete(item);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemParkingTypeVM>> GetAllWhereAsync(int take, int page = 1)
        {
            ICollection<ParkingType> items = await _repository
                .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemParkingTypeVM> vMs = _mapper.Map<ICollection<ItemParkingTypeVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemParkingTypeVM>> GetAllWhereByOrderAsync(int take, Expression<Func<ParkingType, object>>? orderExpression, int page = 1)
        {
            ICollection<ParkingType> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemParkingTypeVM> vMs = _mapper.Map<ICollection<ItemParkingTypeVM>>(items);

            return vMs;
        }

        public async Task<GetParkingTypeVM> GetByIdAsync(int id, int take, int page = 1)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(ParkingType.ProductParkingTypes)}" };
            ParkingType item = await _repository.GetByIdPaginatedAsync(id, take: take, skip: (page - 1) * take, IsTracking: false, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            GetParkingTypeVM get = _mapper.Map<GetParkingTypeVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ParkingType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = false;
            await _repository.SaveChanceAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ParkingType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChanceAsync();
        }

        public async Task<UpdateParkingTypeVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ParkingType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdateParkingTypeVM update = _mapper.Map<UpdateParkingTypeVM>(item);

            return update;
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateParkingTypeVM update, ModelStateDictionary model)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            ParkingType item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            if (await _repository.CheckUniqueAsync(x => x.Name == update.Name))
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
