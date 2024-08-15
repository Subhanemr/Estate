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
using System.Security.Claims;

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

        public async Task CreatePopulateDropdowns(CreateClientVM create)
        {
            create.Corporates = _mapper.Map<ICollection<IncludeCorporateVM>>(await _corporateRepository.GetAll().ToListAsync());
        }
        public async Task UpdatePopulateDropdowns(UpdateClientVM update)
        {
            update.Corporates = _mapper.Map<ICollection<IncludeCorporateVM>>(await _corporateRepository.GetAll().ToListAsync());
        }

        public async Task<bool> CreateAsync(CreateClientVM create, ModelStateDictionary model)
        {
            if (!model.IsValid)
            {
                await CreatePopulateDropdowns(create);
                return false;
            }

            if (!await _corporateRepository.CheckUniqueAsync(x => x.Id == create.CorporateId))
            {
                await CreatePopulateDropdowns(create);
                model.AddModelError("CorporateId", "Corporate not found");
                return false;
            }
            Client item = _mapper.Map<Client>(create);
            item.AppUserId = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _repository.AddAsync(item);
            await _repository.SaveChangeAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Client.Corporate)}" };
            Client item = await _getByIdAsync(id, includes: includes);
            _repository.Delete(item);
            await _repository.SaveChangeAsync();
        }

        public async Task<ICollection<ItemClientVM>> GetAllWhereAsync(int take, int page)
        {
            string[] includes = { $"{nameof(Client.Corporate)}" };
            ICollection<Client> items = await _repository
                    .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemClientVM> vMs = _mapper.Map<ICollection<ItemClientVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemClientVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Client, object>>? orderExpression, int page)
        {
            string[] includes = { $"{nameof(Client.Corporate)}" };
            ICollection<Client> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemClientVM> vMs = _mapper.Map<ICollection<ItemClientVM>>(items);

            return vMs;
        }

        public async Task<PaginationVM<ItemClientVM>> GetFilteredAsync(string? search, int take, int page, int order, bool isDeleted = false)
        {
            if (page <= 0)
                throw new WrongRequestException("Invalid page number.");
            if (take <= 0)
                throw new WrongRequestException("Invalid take value.");
            if (order <= 0)
                throw new WrongRequestException("Invalid order value.");

            string[] includes = { $"{nameof(Client.Corporate)}", $"{nameof(Client.AppUser)}" };
            double count = await _repository
                .CountAsync(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Comment.ToLower().Contains(search.ToLower()) : true, false);

            ICollection<Client> items = new List<Client>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Comment.ToLower().Contains(search.ToLower()) : true,
                        x => x.Comment, false, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Comment.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Comment.ToLower().Contains(search.ToLower()) : true,
                        x => x.Comment, true, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Comment.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }

            ICollection<ItemClientVM> vMs = _mapper.Map<ICollection<ItemClientVM>>(items);

            PaginationVM<ItemClientVM> pagination = new PaginationVM<ItemClientVM>
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

        public async Task<GetClientVM> GetByIdAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Client.Corporate)}", $"{nameof(Client.AppUser)}" };
            Client item = await _getByIdAsync(id, false, includes);

            GetClientVM get = _mapper.Map<GetClientVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Client item = await _getByIdAsync(id);

            item.IsDeleted = false;
            await _repository.SaveChangeAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Client item = await _getByIdAsync(id);

            item.IsDeleted = true;
            await _repository.SaveChangeAsync();
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateClientVM update, ModelStateDictionary model)
        {
            if (!model.IsValid)
            {
                await UpdatePopulateDropdowns(update);
                return false;
            }

            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Client.Corporate)}" };
            Client item = await _getByIdAsync(id, includes: includes);

            if (!await _corporateRepository.CheckUniqueAsync(x => x.Id == update.CorporateId))
            {
                await UpdatePopulateDropdowns(update);
                model.AddModelError("CorporateId", "Corporate not found");
                return false;
            }

            _mapper.Map(update, item);
            await _repository.SaveChangeAsync();

            return true;
        }

        public async Task<UpdateClientVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Client.Corporate)}" };
            Client item = await _getByIdAsync(id, includes: includes);

            UpdateClientVM update = _mapper.Map<UpdateClientVM>(item);
            await UpdatePopulateDropdowns(update);

            return update;
        }

        private async Task<Client> _getByIdAsync(int id, bool isTracking = true, params string[] includes)
        {
            Client client = await _repository.GetByIdAsync(id, isTracking, includes);
            if (client is null)
                throw new NotFoundException($"Client not found({id})!");

            return client;
        }
    }
}
