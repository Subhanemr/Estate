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
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _repository;
        private readonly IHttpContextAccessor _http;
        private readonly IWebHostEnvironment _env;
        private readonly ICLoudService _cLoud;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepository _productRepository;

        public BlogService(IMapper mapper, IBlogRepository repository, IHttpContextAccessor http,
            IWebHostEnvironment env, ICLoudService cLoud, UserManager<AppUser> userManager, IProductRepository productRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _http = http;
            _env = env;
            _cLoud = cLoud;
            _userManager = userManager;
            _productRepository = productRepository;
        }

        public async Task<bool> CreateAsync(CreateBlogVM create, ModelStateDictionary model)
        {
            if (!model.IsValid) return false;
            if (await _repository.CheckUniqueAsync(x => x.Name.ToLower().Trim() == create.Name.ToLower().Trim()))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }

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
                //CreatedBy = _http.HttpContext.User.Identity.Name,
                IsPrimary = true,
                Url = await _cLoud.FileCreateAsync(create.MainPhoto)
                //Url = await create.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images")
            };
            if (!create.HoverPhoto.ValidateType())
            {
                model.AddModelError("HoverPhoto", "File Not supported");
                return false;
            }
            if (!create.HoverPhoto.ValidataSize())
            {
                model.AddModelError("HoverPhoto", "Image should not be larger than 10 mb");
                return false;
            }
            BlogImage hoverImage = new BlogImage
            {
                //CreatedBy = _http.HttpContext.User.Identity.Name,
                IsPrimary = false,
                Url = await _cLoud.FileCreateAsync(create.HoverPhoto)
                //Url = await create.HoverPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images")
            };
            Blog item = _mapper.Map<Blog>(create);

            item.BlogImages = new List<BlogImage> { mainImage, hoverImage };

            //item.CreatedBy = _http.HttpContext.User.Identity.Name;

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
            foreach (var image in item.BlogImages)
            {
                await _cLoud.FileDeleteAsync(image.Url);
                //image.Url.DeleteFile(_env.WebRootPath, "assets", "images");
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

        public async Task<PaginationVM<ItemBlogVM>> GetFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes = { $"{nameof(Blog.BlogImages)}" };

            double count = await _repository.CountAsync();

            ICollection<Blog> items = new List<Blog>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, false, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, true, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, false, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }

            ICollection<ItemBlogVM> vMs = _mapper.Map<ICollection<ItemBlogVM>>(items);

            PaginationVM<ItemBlogVM> pagination = new PaginationVM<ItemBlogVM>
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

        public async Task<PaginationVM<ItemBlogVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes = { $"{nameof(Blog.BlogImages)}" };

            double count = await _repository.CountAsync();

            ICollection<Blog> items = new List<Blog>();

            switch (order)
            {
                case 1:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, false, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 2:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, false, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 3:
                    items = await _repository
                    .GetAllWhereByOrder(x => x.IsDeleted == true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                        x => x.Name, true, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
                case 4:
                    items = await _repository
                     .GetAllWhereByOrder(x => x.IsDeleted == true && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true,
                      x => x.CreateAt, true, true, (page - 1) * take, take, false, includes).ToListAsync();
                    break;
            }

            ICollection<ItemBlogVM> vMs = _mapper.Map<ICollection<ItemBlogVM>>(items);

            PaginationVM<ItemBlogVM> pagination = new PaginationVM<ItemBlogVM>
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

        public async Task<GetBlogVM> GetByIdAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Blog.BlogComments)}.{nameof(BlogComment.BlogReplies)}",
                $"{nameof(Blog.BlogImages)}" };
            Blog item = await _repository.GetByIdAsync(id, IsTracking: false, includes: includes);
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

            if (await _repository.CheckUniqueAsync(x => x.Name.ToLower().Trim() == update.Name.ToLower().Trim() && x.Id != id))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }

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
                BlogImage main = item.BlogImages.FirstOrDefault(x => x.IsPrimary == true);
                await _cLoud.FileDeleteAsync(main.Url);
                //main.Url.DeleteFile(_env.WebRootPath, "assets", "images");
                item.BlogImages.Add(new BlogImage
                {
                    //CreatedBy = _http.HttpContext.User.Identity.Name,
                    IsPrimary = true,
                    Url = await _cLoud.FileCreateAsync(update.MainPhoto)
                    //Url = await update.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images")
                });
            }


            if (update.HoverPhoto != null)
            {
                if (!update.HoverPhoto.ValidateType())
                {
                    model.AddModelError("HoverPhoto", "File Not supported");
                    return false;
                }
                if (!update.HoverPhoto.ValidataSize())
                {
                    model.AddModelError("HoverPhoto", "Image should not be larger than 10 mb");
                    return false;
                }
                BlogImage hover = item.BlogImages.FirstOrDefault(x => x.IsPrimary == false);
                await _cLoud.FileDeleteAsync(hover.Url);
                //main.Url.DeleteFile(_env.WebRootPath, "assets", "images");
                item.BlogImages.Add(new BlogImage
                {
                    //CreatedBy = _http.HttpContext.User.Identity.Name,
                    IsPrimary = false,
                    Url = await _cLoud.FileCreateAsync(update.HoverPhoto)
                    //Url = await update.HoverPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images")
                });
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
