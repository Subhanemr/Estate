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
using System.Linq.Expressions;

namespace Estate.Persistance.Implementations.Services
{
    public class CorporateService : ICorporateService
    {
        private readonly IMapper _mapper;
        private readonly ICorporateRepository _repository;
        private readonly IHttpContextAccessor _http;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;

        public CorporateService(IMapper mapper, ICorporateRepository repository,
            IHttpContextAccessor http, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _http = http;
            _env = env;
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(CreateCorporateVM create, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            if (await _repository.CheckUniqueAsync(x => x.CorporateLink == create.CorporateLink))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }
            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

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

            item.Img = await create.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img");
            item.CreatedBy = user.UserName;

            await _repository.AddAsync(item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Corporate.Clients)}" };
            Corporate item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");
            item.Img.DeleteFile(_env.WebRootPath, "assets", "img");
            _repository.Delete(item);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemCorporateVM>> GetAllWhereAsync(int take, int page = 1)
        {
            string[] includes = { $"{nameof(Corporate.Clients)}" };
            ICollection<Corporate> items = await _repository
                    .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemCorporateVM> vMs = _mapper.Map<ICollection<ItemCorporateVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemCorporateVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Corporate, object>>? orderExpression, int page = 1)
        {
            string[] includes = { $"{nameof(Corporate.Clients)}" };
            ICollection<Corporate> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemCorporateVM> vMs = _mapper.Map<ICollection<ItemCorporateVM>>(items);

            return vMs;
        }

        public async Task<GetCorporateVM> GetByIdAsync(int id, int take, int page = 1)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Corporate.Clients)}" };
            Corporate item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            GetCorporateVM get = _mapper.Map<GetCorporateVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Corporate item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = false;
            await _repository.SaveChanceAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Corporate item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChanceAsync();
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateCorporateVM update, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            if (await _repository.CheckUniqueAsync(x => x.CorporateLink == update.CorporateLink))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }
            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={ $"{nameof(Corporate.Clients)}" };
            Corporate item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

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

                item.Img.DeleteFile(_env.WebRootPath, "assets", "img");
                item.Img = await update.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img");
            }


            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateCorporateVM, Corporate>()
                    .ForMember(dest => dest.Img, opt => opt.Ignore());
            });
            var mapper = config.CreateMapper();

            mapper.Map(update, item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task<UpdateCorporateVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Corporate.Clients)}" };
            Corporate item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdateCorporateVM update = _mapper.Map<UpdateCorporateVM>(item);

            return update;
        }
    }
}
