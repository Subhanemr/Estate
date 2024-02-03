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
    public class FeaturesService : IFeaturesService
    {
        private readonly IMapper _mapper;
        private readonly IFeaturesRepository _repository;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<AppUser> _userManager;

        public FeaturesService(IMapper mapper, IFeaturesRepository repository,
            IHttpContextAccessor http, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _http = http;
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(CreateFeaturesVM create, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            if (await _repository.CheckUniqueAsync(x => x.Name == create.Name))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }
            Features item = _mapper.Map<Features>(create);
            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);
            item.CreatedBy = user.UserName;

            await _repository.AddAsync(item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Features item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            _repository.Delete(item);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemFeaturesVM>> GetAllWhereAsync(int take, int page = 1)
        {
            ICollection<Features> items = await _repository
                .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemFeaturesVM> vMs = _mapper.Map<ICollection<ItemFeaturesVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemFeaturesVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Features, object>>? orderExpression, int page = 1)
        {
            ICollection<Features> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemFeaturesVM> vMs = _mapper.Map<ICollection<ItemFeaturesVM>>(items);

            return vMs;
        }

        public async Task<GetFeaturesVM> GetByIdAsync(int id, int take, int page = 1)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Features.ProductFeatures)}" };
            Features item = await _repository.GetByIdPaginatedAsync(id, take: take, skip: (page - 1) * take, IsTracking: false, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            GetFeaturesVM get = _mapper.Map<GetFeaturesVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Features item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = false;
            await _repository.SaveChanceAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Features item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChanceAsync();
        }

        public async Task<UpdateFeaturesVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Features item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdateFeaturesVM update = _mapper.Map<UpdateFeaturesVM>(item);

            return update;
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateFeaturesVM update, ModelStateDictionary model)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Features item = await _repository.GetByIdAsync(id);
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
