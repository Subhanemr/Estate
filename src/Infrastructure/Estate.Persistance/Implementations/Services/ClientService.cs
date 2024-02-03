using AutoMapper;
using Estate.Application.Abstractions.Repositories;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels.Client;
using Estate.Application.ViewModels.Corporate;
using Estate.Domain.Entities;
using Estate.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Estate.Persistance.Implementations.Services
{
    public class ClientService : IClientService
    {
        private readonly IMapper _mapper;
        private readonly IClientRepository _repository;
        private readonly ICorporateRepository _corporateRepository;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<AppUser> _userManager;

        public ClientService(IMapper mapper, IClientRepository repository, ICorporateRepository corporateRepository, 
            IHttpContextAccessor http, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _corporateRepository = corporateRepository;
            _http = http;
            _userManager = userManager;
        }

        public async void CreatePopulateDropdowns(CreateClientVM create)
        {
            create.Corporates = _mapper.Map<ICollection<IncludeCorporateVM>>(await _corporateRepository.GetAll().ToListAsync());
        }
        public async void UpdatePopulateDropdowns(UpdateClientVM update)
        {
            update.Corporates = _mapper.Map<ICollection<IncludeCorporateVM>>(await _corporateRepository.GetAll().ToListAsync());
        }

        public async Task<bool> CreateAsync(CreateClientVM create, ModelStateDictionary model)
        {
            if (!model.IsValid)
            {
                CreatePopulateDropdowns(create);
                return false;
            }

            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

            if (!await _corporateRepository.CheckUniqueAsync(x => x.Id == create.CorporateId))
            {
                CreatePopulateDropdowns(create);
                model.AddModelError("CorporateId", "Corporate not found");
                return false;
            }
            Client item = _mapper.Map<Client>(create);

            item.CreatedBy = user.UserName;

            await _repository.AddAsync(item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Client.Corporate)}" };
            Client item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");
            _repository.Delete(item);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemClientVM>> GetAllWhereAsync(int take, int page = 1)
        {
            string[] includes = { $"{nameof(Client.Corporate)}" };
            ICollection<Client> items = await _repository
                    .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemClientVM> vMs = _mapper.Map<ICollection<ItemClientVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemClientVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Client, object>>? orderExpression, int page = 1)
        {
            string[] includes = { $"{nameof(Client.Corporate)}" };
            ICollection<Client> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemClientVM> vMs = _mapper.Map<ICollection<ItemClientVM>>(items);

            return vMs;
        }

        public async Task<GetClientVM> GetByIdAsync(int id, int take, int page = 1)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Client.Corporate)}" };
            Client item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            GetClientVM get = _mapper.Map<GetClientVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Client item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = false;
            await _repository.SaveChanceAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Client item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChanceAsync();
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateClientVM update, ModelStateDictionary model)
        {
            if (!model.IsValid)
            {
                UpdatePopulateDropdowns(update);
                return false;
            }

            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Client.Corporate)}" };
            Client item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            if (!await _corporateRepository.CheckUniqueAsync(x => x.Id == update.CorporateId))
            {
                UpdatePopulateDropdowns(update);
                model.AddModelError("CorporateId", "Corporate not found");
                return false;
            }

            _mapper.Map(update, item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task<UpdateClientVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Client.Corporate)}" };
            Client item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdateClientVM update = _mapper.Map<UpdateClientVM>(item);
            UpdatePopulateDropdowns(update);

            return update;
        }
    }
}
