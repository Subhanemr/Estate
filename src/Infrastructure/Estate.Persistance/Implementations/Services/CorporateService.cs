using AutoMapper;
using Estate.Application.Abstractions.Repositories;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Estate.Infrastructure.Exceptions;
using Estate.Infrastructure.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Estate.Persistance.Implementations.Services
{
    public class CorporateService : ICorporateService
    {
        private readonly IMapper _mapper;
        private readonly ICorporateRepository _repository;
        private readonly IHttpContextAccessor _http;
        private readonly IWebHostEnvironment _env;
        private readonly ICLoudService _cLoud;

        public CorporateService(IMapper mapper, ICorporateRepository repository,
            IHttpContextAccessor http, IWebHostEnvironment env, ICLoudService cLoud)
        {
            _mapper = mapper;
            _repository = repository;
            _http = http;
            _env = env;
            _cLoud = cLoud;
        }

        public async Task<bool> CreateAsync(CreateCorporateVM create, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            if (await _repository.CheckUniqueAsync(x => x.CorporateLink.ToLower().Trim() == create.CorporateLink.ToLower().Trim()))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }

            if (!create.Photo.ValidateType())
            {
                model.AddModelError("Photo", "File Not supported");
                return false;
            }
            if (!create.Photo.ValidataSize())
            {
                model.AddModelError("Photo", "Image should not be larger than 10 mb");
                return false;
            }

            Corporate item = _mapper.Map<Corporate>(create);

            item.Img = await _cLoud.FileCreateAsync(create.Photo);
            //item.Img = await create.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images");

            await _repository.AddAsync(item);
            await _repository.SaveChangeAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Corporate.Clients)}" };
            Corporate item = await _getByIdAsync(id, includes: includes);

            await _cLoud.FileDeleteAsync(item.Img);
            //item.Img.DeleteFile(_env.WebRootPath, "assets", "images");
            _repository.Delete(item);
            await _repository.SaveChangeAsync();
        }

        public async Task<ICollection<ItemCorporateVM>> GetAllWhereAsync(int take, int page)
        {
            string[] includes = { $"{nameof(Corporate.Clients)}.{nameof(Client.AppUser)}" };
            ICollection<Corporate> items = await _repository
                    .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemCorporateVM> vMs = _mapper.Map<ICollection<ItemCorporateVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemCorporateVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Corporate, object>>? orderExpression, int page)
        {
            string[] includes = { $"{nameof(Corporate.Clients)}.{nameof(Client.AppUser)}" };
            ICollection<Corporate> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemCorporateVM> vMs = _mapper.Map<ICollection<ItemCorporateVM>>(items);

            return vMs;
        }

        public async Task<PaginationVM<ItemCorporateVM>> GetFilteredAsync(string? search, int take, int page, int order, bool isDeleted = false)
        {
            if (page <= 0)
                throw new WrongRequestException("Invalid page number.");
            if (take <= 0)
                throw new WrongRequestException("Invalid take value.");
            if (order <= 0)
                throw new WrongRequestException("Invalid order value.");

            string[] includes = { $"{nameof(Corporate.Clients)}.{nameof(Client.AppUser)}" };
            double count = await _repository
                .CountAsync(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true, false);

            ICollection<Corporate> items = new List<Corporate>();

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
                      x => x.CreateAt, false, isDeleted, (page - 1) * take, take: take, false, includes).ToListAsync();
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

            ICollection<ItemCorporateVM> vMs = _mapper.Map<ICollection<ItemCorporateVM>>(items);

            PaginationVM<ItemCorporateVM> pagination = new PaginationVM<ItemCorporateVM>
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

        public async Task<GetCorporateVM> GetByIdAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Corporate.Clients)}.{nameof(Client.AppUser)}" };
            Corporate item = await _getByIdAsync(id, false, includes);

            GetCorporateVM get = _mapper.Map<GetCorporateVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Corporate item = await _getByIdAsync(id);

            item.IsDeleted = false;
            await _repository.SaveChangeAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Corporate item = await _getByIdAsync(id);

            item.IsDeleted = true;
            await _repository.SaveChangeAsync();
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateCorporateVM update, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            if (await _repository.CheckUniqueAsync(x => x.CorporateLink.ToLower().Trim() == update.CorporateLink.ToLower().Trim() && x.Id != id))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }

            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Corporate.Clients)}" };
            Corporate item = await _getByIdAsync(id, includes: includes);

            if (update.Photo != null)
            {
                if (!update.Photo.ValidateType())
                {
                    model.AddModelError("Photo", "File Not supported");
                    return false;
                }
                if (!update.Photo.ValidataSize())
                {
                    model.AddModelError("Photo", "Image should not be larger than 10 mb");
                    return false;
                }

                await _cLoud.FileDeleteAsync(item.Img);
                item.Img = await _cLoud.FileCreateAsync(update.Photo);

                //item.Img.DeleteFile(_env.WebRootPath, "assets", "images");
                //item.Img = await update.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images");
            }


            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateCorporateVM, Corporate>()
                    .ForMember(dest => dest.Img, opt => opt.Ignore());
            });
            var mapper = config.CreateMapper();

            mapper.Map(update, item);
            await _repository.SaveChangeAsync();

            return true;
        }

        public async Task<UpdateCorporateVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Corporate.Clients)}" };
            Corporate item = await _getByIdAsync(id, includes: includes);

            UpdateCorporateVM update = _mapper.Map<UpdateCorporateVM>(item);

            return update;
        }

        private async Task<Corporate> _getByIdAsync(int id, bool isTracking = true, params string[] includes)
        {
            Corporate corporate = await _repository.GetByIdAsync(id, isTracking, includes);
            if (corporate is null)
                throw new NotFoundException($"Corporate not found({id})!");

            return corporate;
        }
    }
}
