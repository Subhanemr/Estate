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
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.RegularExpressions;

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

        public BlogService(IMapper mapper, IBlogRepository repository, IHttpContextAccessor http,
            IWebHostEnvironment env, ICLoudService cLoud, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _repository = repository;
            _http = http;
            _env = env;
            _cLoud = cLoud;
            _userManager = userManager;
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
                IsPrimary = false,
                Url = await _cLoud.FileCreateAsync(create.HoverPhoto)
                //Url = await create.HoverPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images")
            };
            Blog item = _mapper.Map<Blog>(create);

            item.BlogImages = new List<BlogImage> { mainImage, hoverImage };

            await _repository.AddAsync(item);
            await _repository.SaveChangeAsync();

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
            await _repository.SaveChangeAsync();
        }

        public async Task<ICollection<ItemBlogVM>> GetAllWhereAsync(int take, int page)
        {
            string[] includes ={
                $"{nameof(Blog.BlogComments)}.{nameof(BlogComment.BlogReplies)}",
                $"{nameof(Blog.BlogImages)}" };
            ICollection<Blog> items = await _repository
                    .GetAllWhere(skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemBlogVM> vMs = _mapper.Map<ICollection<ItemBlogVM>>(items);

            return vMs;
        }

        public async Task<ICollection<ItemBlogVM>> GetAllWhereByOrderAsync(int take, int page, Expression<Func<Blog, object>>? orderExpression,
            Expression<Func<Blog, bool>>? expression = null)
        {
            string[] includes = { $"{nameof(Blog.BlogImages)}" };
            ICollection<Blog> items = await _repository
                    .GetAllWhereByOrder(expression, orderExpression, skip: (page - 1) * take, take: take, IsTracking: false, includes: includes).ToListAsync();

            ICollection<ItemBlogVM> vMs = _mapper.Map<ICollection<ItemBlogVM>>(items);

            return vMs;
        }

        public async Task<PaginationVM<ItemBlogVM>> GetFilteredAsync(string? search, int take, int page, int order, bool isDeleted = false)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            string[] includes = { $"{nameof(Blog.BlogImages)}" };

            double count = await _repository
                .CountAsync(x => x.IsDeleted == false && !string.IsNullOrEmpty(search) ? x.Name.ToLower().Contains(search.ToLower()) : true, false);

            ICollection<Blog> items = new List<Blog>();

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
                      x => x.CreateAt, false, isDeleted, (page - 1) * take, take, false, includes).ToListAsync();
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
                $"{nameof(Blog.BlogComments)}.{nameof(BlogComment.BlogReplies)}.{nameof(BlogReply.AppUser)}",
                $"{nameof(Blog.BlogComments)}.{nameof(BlogComment.AppUser)}",
                $"{nameof(Blog.BlogImages)}" };
            Blog item = await _repository.GetByIdAsync(id, false, includes);
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
            await _repository.SaveChangeAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Blog item = await _repository.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Your request was not found");

            item.IsDeleted = true;
            await _repository.SaveChangeAsync();
        }

        public async Task<bool> UpdatePostAsync(int id, UpdateBlogVM update, ModelStateDictionary model)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            string[] includes ={
                $"{nameof(Blog.BlogComments)}.{nameof(BlogComment.BlogReplies)}",
                $"{nameof(Blog.BlogImages)}" };
            Blog item = await _repository.GetByIdAsync(id, includes: includes);
            update.Images = _mapper.Map<ICollection<IncludeBlogImageVM>>(item.BlogImages);
            if (item == null) throw new NotFoundException("Your request was not found");

            if (!model.IsValid) return false;

            if (await _repository.CheckUniqueAsync(x => x.Name.ToLower().Trim() == update.Name.ToLower().Trim() && x.Id != id))
            {
                model.AddModelError("Name", "Name is exists");
                return false;
            }

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
            await _repository.SaveChangeAsync();

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

        public async Task<bool> CommentAsync(int blogId, string comment, ITempDataDictionary tempData)
        {
            tempData["Comment"] = "";

            if (string.IsNullOrWhiteSpace(comment))
            {
                tempData["Comment"] += $"<p class=\"text-danger\" style=\"color: red;\">Comment is required</p>";
                return false;
            }
            if (comment.Length > 1500)
            {
                tempData["Comment"] += $"<p class=\"text-danger\" style=\"color: red;\">Comment max characters is 1-1500</p>";
                return false;
            }
            if (!Regex.IsMatch(comment, @"^[A-Za-z0-9\s,\.]+$"))
            {
                tempData["Comment"] += $"<p class=\"text-danger\" style=\" color: red;\">Comment can only contain letters, numbers, spaces, commas, and periods.</p>";
                return false;
            }
            BlogComment blogComment = new BlogComment
            {
                Comment = comment,
                BlogId = blogId,
                AppUserId = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            await _repository.AddComment(blogComment);
            await _repository.SaveChangeAsync();
            return true;
        }
        public async Task<bool> ReplyAsync(int blogCommnetId, string comment, ITempDataDictionary tempData)
        {
            tempData["Reply"] = "";

            if (string.IsNullOrWhiteSpace(comment))
            {
                tempData["Reply"] += $"<p class=\"text-danger\" style=\"color: red;\">Comment is required</p>";
                return false;
            }
            if (comment.Length > 1500)
            {
                tempData["Reply"] += $"<p class=\"text-danger\" style=\"color: red;\">Comment max characters is 1-1500</p>";
                return false;
            }
            if (!Regex.IsMatch(comment, @"^[A-Za-z0-9\s,\.]+$"))
            {
                tempData["Reply"] += $"<p class=\"text-danger\" style=\" color: red;\">Comment can only contain letters, numbers, spaces, commas, and periods.</p>";
                return false;
            }
            BlogReply blogComment = new BlogReply
            {
                ReplyComment = comment,
                BlogCommentId = blogCommnetId,
                AppUserId = _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            await _repository.AddReply(blogComment);
            await _repository.SaveChangeAsync();
            return true;
        }
    }
}
