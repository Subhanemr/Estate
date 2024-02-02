using AutoMapper;
using Estate.Application.Abstractions.Repositories;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels.Blog;
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
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _repository;
        private readonly IBlogNameRepository _nameRepository;
        private readonly IBlogImageRepository _blogImageRepository;
        private readonly IHttpContextAccessor _http;
        private readonly IWebHostEnvironment _env;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        private readonly UserManager<AppUser> _userManager;

        public BlogService(IMapper mapper, IBlogRepository repository, IBlogNameRepository nameRepository, 
            IBlogImageRepository blogImageRepository, IHttpContextAccessor http, IWebHostEnvironment env, 
            ITempDataDictionaryFactory tempDataDictionaryFactory, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _nameRepository = nameRepository;
            _blogImageRepository = blogImageRepository;
            _http = http;
            _env = env;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(CreateBlogVM create, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;

            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

            if (!create.MainPhoto.ValidateType())
            {
                model.AddModelError("MainPhoto", "File Not supported");
                return false;
            }
            if (!create.MainPhoto.ValidataSize())
            {
                model.AddModelError("MainPhoto", "Image should not be larger than 10 mb");
                return false;
            }
            BlogImage mainImage = new BlogImage
            {
                CreatedBy = user.UserName,
                IsPrimary = true,
                Url = await create.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images")
            };
            Blog item = _mapper.Map<Blog>(create);

            item.BlogImages = new List<BlogImage> { mainImage };

            if (item.BlogImages == null) item.BlogImages = new List<BlogImage>();

            var httpContext = _http.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);

            tempData["Message"] = "";

            foreach (var photo in create.Photos)
            {
                if (!photo.ValidateType())
                {
                    tempData["Message"] += $"<p class=\"text-danger\">{photo.Name} type is not suitable</p>";
                    continue;
                }

                if (!photo.ValidataSize(10))
                {
                    tempData["Message"] += $"<p class=\"text-danger\">{photo.Name} the size is not suitable</p>";
                    continue;
                }

                item.BlogImages.Add(new BlogImage
                {
                    CreatedBy = user.UserName,
                    IsPrimary = null,
                    Url = await photo.CreateFileAsync(_env.WebRootPath, "assets", "images")
                });
            }
            item.CreatedBy = user.UserName;

            await _repository.AddAsync(item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Blog.BlogComments)}.{nameof(BlogComment.BlogReplies)}",
                $"{nameof(Blog.BlogImages)}" };
            Blog item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");
            foreach (BlogImage image in item.BlogImages)
            {
                image.Url.DeleteFile(_env.WebRootPath, "assets", "images");
            }
            _repository.Delete(item);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemBlogVM>> GetAllWhereAsync(int take, int page = 1)
        {
            string[] includes ={
                $"{nameof(Blog.BlogComments)}.{nameof(BlogComment.BlogReplies)}",
                $"{nameof(Blog.BlogImages)}" };
            ICollection<Blog> items = await _repository
                    .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemBlogVM> vMs = _mapper.Map<ICollection<ItemBlogVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemBlogVM>> GetAllWhereByOrderAsync(int take, Expression<Func<Blog, object>>? orderExpression, int page = 1)
        {
            string[] includes = { $"{nameof(Blog.BlogImages)}" };
            ICollection<Blog> items = await _repository
                    .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemBlogVM> vMs = _mapper.Map<ICollection<ItemBlogVM>>(items);

            return vMs;
        }

        public async Task<GetBlogVM> GetByIdAsync(int id, int take, int page = 1)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Blog.BlogComments)}.{nameof(BlogComment.BlogReplies)}",
                $"{nameof(Blog.BlogImages)}" };
            Blog item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            GetBlogVM get = _mapper.Map<GetBlogVM>(item);

            return get;
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Blog item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = false;
            await _repository.SaveChanceAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Blog item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChanceAsync();
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateBlogVM update, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);

            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Blog.BlogComments)}.{nameof(BlogComment.BlogReplies)}",
                $"{nameof(Blog.BlogImages)}" };
            Blog item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            if (update.MainPhoto != null)
            {
                if (!update.MainPhoto.ValidateType())
                {
                    model.AddModelError("MainPhoto", "File Not supported");
                    return false;
                }
                if (!update.MainPhoto.ValidataSize())
                {
                    model.AddModelError("MainPhoto", "Image should not be larger than 10 mb");
                    return false;
                }
            }
            if (update.MainPhoto != null)
            {
                BlogImage main = item.BlogImages.FirstOrDefault(x => x.IsPrimary == true);
                main.Url.DeleteFile(_env.WebRootPath, "assets", "images");
                _blogImageRepository.Delete(main);
                item.BlogImages.Add(new BlogImage
                {
                    CreatedBy = user.UserName,
                    IsPrimary = true,
                    Url = await update.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images")
                });
            }

            if (item.BlogImages == null) item.BlogImages = new List<BlogImage>();

            if (update.ImageIds == null) update.ImageIds = new List<int>();

            ICollection<BlogImage> remove = item.BlogImages
                    .Where(pi => pi.IsPrimary == null && !update.ImageIds.Exists(imgId => imgId == pi.Id)).ToList();

            foreach (var image in remove)
            {
                image.Url.DeleteFile(_env.WebRootPath, "assets", "images");
                item.BlogImages.Remove(image);
            }

            var httpContext = _http.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);

            tempData["Message"] = "";

            if (update.Photos != null)
            {
                foreach (var photo in update.Photos)
                {
                    if (!photo.ValidateType())
                    {
                        tempData["Message"] += $"<p class=\"text-danger\">{photo.Name} type is not suitable</p>";
                        continue;
                    }

                    if (!photo.ValidataSize(10))
                    {
                        tempData["Message"] += $"<p class=\"text-danger\">{photo.Name} the size is not suitable</p>";
                        continue;
                    }

                    item.BlogImages.Add(new BlogImage
                    {
                        CreatedBy = user.UserName,
                        IsPrimary = null,
                        Url = await photo.CreateFileAsync(_env.WebRootPath, "assets", "images")
                    });
                }
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateBlogVM, Blog>()
                    .ForMember(dest => dest.BlogImages, opt => opt.Ignore());
            });
            var mapper = config.CreateMapper();

            mapper.Map(update, item);
            await _repository.SaveChanceAsync();

            return true;
        }

        public async Task<UpdateBlogVM> UpdateAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Blog.BlogComments)}.{nameof(BlogComment.BlogReplies)}",
                $"{nameof(Blog.BlogImages)}" };
            Blog item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new NotFoundException("Your request was not found");

            UpdateBlogVM update = _mapper.Map<UpdateBlogVM>(item);

            return update;
        }
    }
}
