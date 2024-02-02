using AutoMapper;
using Estate.Application.Abstractions.Repositories;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels.Category;
using Estate.Domain.Entities;
using Estate.Infrastructure.Exceptions;
using Estate.Infrastructure.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Estate.Persistance.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;
        private readonly ICategoryNameRepository _nameRepository;
        private readonly IHttpContextAccessor _http;
        private readonly IWebHostEnvironment _env;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        private readonly UserManager<AppUser> _userManager;

        public CategoryService(IMapper mapper, ICategoryRepository repository, ICategoryNameRepository nameRepository, 
            IHttpContextAccessor http, IWebHostEnvironment env, ITempDataDictionaryFactory tempDataDictionaryFactory, 
            UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _nameRepository = nameRepository;
            _http = http;
            _env = env;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(CreateCategoryVM create, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;

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

            Category item = _mapper.Map<Category>(create);

            item.Img = await create.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img");
            item.CreatedBy = user.UserName;

            await _repository.AddAsync(item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={ $"{nameof(Category.Products)}" };
            Category item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.Img.DeleteFile(_env.WebRootPath, "assets", "img");

            _repository.Delete(item);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemCategoryVM>> GetAllWhereAsync(int take, int page = 1)
        {
            string[] includes = { $"{nameof(Category.Products)}" };

            ICollection<Category> items = await _repository
                    .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemCategoryVM> vMs = _mapper.Map<ICollection<ItemCategoryVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemCategoryVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Category, object>>? orderExpression, int page = 1)
        {
            string[] includes = { $"{nameof(Category.Products)}" };

            ICollection<Category> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemCategoryVM> vMs = _mapper.Map<ICollection<ItemCategoryVM>>(items);

            return vMs;
        }

        public async Task<GetCategoryVM> GetByIdAsync(int id, int take, int page = 1)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Category.Products)}" };

            Category item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            GetCategoryVM get = _mapper.Map<GetCategoryVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Category item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = false;
            await _repository.SaveChanceAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Category item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChanceAsync();
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateCategoryVM update, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Category.Products)}" };

            Category item = await _repository.GetByIdAsync(id, includes: includes);
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
                item.Img = await update.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img");
            }
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateCategoryVM, Category>()
                    .ForMember(dest => dest.Img, opt => opt.Ignore());
            });
            var mapper = config.CreateMapper();

            mapper.Map(update, item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task<UpdateCategoryVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes = { $"{nameof(Category.Products)}" };
            Category item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdateCategoryVM update = _mapper.Map<UpdateCategoryVM>(item);

            return update;
        }
    }
}
