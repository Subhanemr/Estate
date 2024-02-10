using AutoMapper;
using Estate.Application.Abstractions.Repositories;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Estate.Domain.Enums;
using Estate.Infrastructure.Exceptions;
using Estate.Infrastructure.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Estate.Persistance.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ICLoudService _cLoud;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IHttpContextAccessor _http;
        private readonly IConfiguration _configuration;


        public UserService(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager,
            ICLoudService cLoud, IEmailService emailService, IHttpContextAccessor http, IConfiguration configuration,
            IAgencyRepository agencyRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _cLoud = cLoud;
            _emailService = emailService;
            _http = http;
            _configuration = configuration;
            _agencyRepository = agencyRepository;
        }

        public async Task<PaginationVM<ItemAppUserVM>> GetFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            double count = await _userManager.Users.CountAsync();

            ICollection<AppUser> users = new List<AppUser>();

            switch (order)
            {
                case 1:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == false).OrderBy(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 2:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == false).OrderByDescending(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 3:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == false).OrderBy(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 4:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == false).OrderByDescending(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
            }

            ICollection<ItemAppUserVM> vMs = _mapper.Map<ICollection<ItemAppUserVM>>(users);

            PaginationVM<ItemAppUserVM> pagination = new PaginationVM<ItemAppUserVM>
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

        public async Task<PaginationVM<ItemAppUserVM>> GetDeleteFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            double count = await _userManager.Users.CountAsync();

            ICollection<AppUser> users = new List<AppUser>();

            switch (order)
            {
                case 1:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == true).OrderBy(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 2:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == true).OrderByDescending(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 3:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == true).OrderBy(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 4:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.UserName != _configuration["AdminSettings:UserName"] && x.UserName != _configuration["ModeratorSettings:UserName"])
                        .Where(x => x.IsActivate == true).OrderByDescending(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
            }

            ICollection<ItemAppUserVM> vMs = _mapper.Map<ICollection<ItemAppUserVM>>(users);

            PaginationVM<ItemAppUserVM> pagination = new PaginationVM<ItemAppUserVM>
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

        public async Task<GetAppUserVM> GetByIdAdminAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.Users
                .Include(x => x.AppUserImages).Include(x => x.Agency)
                .Include(x => x.Favorites).ThenInclude(x => x.Product).ThenInclude(x => x.ProductImages)
                .Include(x => x.Products).ThenInclude(x => x.Category)
                .Include(x => x.Products).ThenInclude(x => x.ProductImages).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) throw new NotFoundException("Your request was not found");

            GetAppUserVM get = _mapper.Map<GetAppUserVM>(user);

            return get;
        }

        public async Task<GetAppUserVM> GetByUserNameAdminAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.Users
                .Include(x => x.AppUserImages).Include(x => x.Agency)
                .Include(x => x.Favorites).ThenInclude(x => x.Product).ThenInclude(x => x.ProductImages)
                .Include(x => x.Products).ThenInclude(x => x.Category)
                .Include(x => x.Products).ThenInclude(x => x.ProductImages).AsNoTracking().FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null) throw new NotFoundException("Your request was not found");

            GetAppUserVM get = _mapper.Map<GetAppUserVM>(user);

            return get;
        }

        public async Task<GetAppUserVM> GetByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.Users
                .Include(x => x.AppUserImages).Include(x => x.Agency)
                .Include(x => x.Favorites).ThenInclude(x => x.Product).ThenInclude(x => x.ProductImages)
                .Include(x => x.Products.Where(a=>a.IsDeleted == false)).ThenInclude(x => x.Category)
                .Include(x => x.Products.Where(a => a.IsDeleted == false)).ThenInclude(x => x.ProductImages).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) throw new NotFoundException("Your request was not found");

            GetAppUserVM get = _mapper.Map<GetAppUserVM>(user);

            return get;
        }

        public async Task<GetAppUserVM> GetByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.Users
                .Include(x => x.AppUserImages).Include(x => x.Agency)
                .Include(x => x.Favorites).ThenInclude(x => x.Product).ThenInclude(x => x.ProductImages)
                .Include(x => x.Products.Where(a => a.IsDeleted == false)).ThenInclude(x => x.Category)
                .Include(x => x.Products.Where(a => a.IsDeleted == false)).ThenInclude(x => x.ProductImages).AsNoTracking().FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null) throw new NotFoundException("Your request was not found");

            GetAppUserVM get = _mapper.Map<GetAppUserVM>(user);

            return get;
        }

        public async Task IsSoulOfAgencyAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            user.SoulOfAgency = true;

            await _userManager.UpdateAsync(user);
        }

        public async Task GiveRoleModeratorAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            await _userManager.AddToRoleAsync(user, UserRoles.Moderator.ToString());
        }
        public async Task DeleteRoleModeratorAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());
        }
        public async Task DeleteRoleAgentAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());
        }
        public async Task ReverseSoftDeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            user.IsActivate = false;

            await _userManager.UpdateAsync(user);
        }

        public async Task SoftDeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            user.IsActivate = true;

            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            await _userManager.DeleteAsync(user);
        }

        public async Task<EditUserVM> EditUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.Users
                .Include(x => x.AppUserImages).Include(x => x.Agency)
                .Include(x => x.Products).ThenInclude(x => x.Category)
                .Include(x => x.Products).ThenInclude(x => x.ProductImages).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) throw new NotFoundException("Your request was not found");

            EditUserVM get = _mapper.Map<EditUserVM>(user);

            return get;
        }

        public async Task<bool> EditUserAsync(string id, EditUserVM update, ModelStateDictionary model)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            _mapper.Map(update, user);
            user.Name = user.Name.Capitalize();
            user.Surname = user.Surname.Capitalize();
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
                await _cLoud.FileDeleteAsync(user.Img);
                user.Img = await _cLoud.FileCreateAsync(update.Photo);
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, false);

            return true;
        }

        public async Task FogotPassword(string id, IUrlHelper url)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var confirmationLink = url.Action("ChangePassword", "User", new { Id = user.Id, Token = token }, _http.HttpContext.Request.Scheme);
            await _emailService.SendMailAsync(user.Email, "Password Reset", confirmationLink);
        }

        public async Task<bool> ChangePassword(string id, string token, FogotPasswordVM fogotPassword, ModelStateDictionary model)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(token)) throw new NotFoundException("Your request was not found");
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            var result = await _userManager.ChangePasswordAsync(user, fogotPassword.Password, fogotPassword.NewPassword);
            if (!result.Succeeded)
            {
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += error.Description;
                }
                throw new WrongRequestException(errors);
            }

            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, false);
            return true;
        }

        public async Task<CreateAppUserAgentVM> BeAAgent(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            CreateAppUserAgentVM create = _mapper.Map<CreateAppUserAgentVM>(user);

            create.Agencys = _mapper.Map<ICollection<IncludeAgencyVM>>(await _agencyRepository.GetAll().ToListAsync());

            return create;
        }

        public async Task<bool> BeAAgentPost(string id, CreateAppUserAgentVM create, ModelStateDictionary model, ITempDataDictionary tempData)
        {
            if (!create.TermsConditions)
            {
                create.Agencys = _mapper.Map<ICollection<IncludeAgencyVM>>(await _agencyRepository.GetAll().ToListAsync());
                model.AddModelError("TermsConditions", "Plese Read and accept rules");
                return false;
            }
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("Your request was not found");

            if (!await _agencyRepository.CheckUniqueAsync(x => x.Id == create.AgencyId))
            {
                create.Agencys = _mapper.Map<ICollection<IncludeAgencyVM>>(await _agencyRepository.GetAll().ToListAsync());
                return false;
            }

            if (!create.MainPhoto.ValidateType())
            {
                create.Agencys = _mapper.Map<ICollection<IncludeAgencyVM>>(await _agencyRepository.GetAll().ToListAsync());
                model.AddModelError("MainPhoto", "File Not supported");
                return false;
            }
            if (!create.MainPhoto.ValidataSize())
            {
                create.Agencys = _mapper.Map<ICollection<IncludeAgencyVM>>(await _agencyRepository.GetAll().ToListAsync());
                model.AddModelError("MainPhoto", "Image should not be larger than 10 mb");
                return false;
            }
            AppUserImage mainImage = new AppUserImage
            {
                //CreatedBy = _http.HttpContext.User.Identity.Name,
                IsPrimary = true,
                Url = await _cLoud.FileCreateAsync(create.MainPhoto)
                //Url = await create.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images")
            };

            _mapper.Map(create, user);

            user.AppUserImages = new List<AppUserImage> { mainImage };

            if (user.AppUserImages == null) user.AppUserImages = new List<AppUserImage>();

            tempData["Message"] = "";

            foreach (IFormFile photo in create.Photos)
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

                user.AppUserImages.Add(new AppUserImage
                {
                    //CreatedBy = _http.HttpContext.User.Identity.Name,
                    IsPrimary = null,
                    Url = await _cLoud.FileCreateAsync(photo)
                    //Url = await photo.CreateFileAsync(_env.WebRootPath, "assets", "images")
                });
            }
            user.IsActivate = true;

            await _emailService.SendMailAsync(user.Email, "Account Check", "We check your account please wait");
            await _emailService.SendMailAsync(_configuration["AdminSettings:Email"], "Account Check", $"{user.UserName} want be a agent");
            await _emailService.SendMailAsync(_configuration["ModeratorSettings:Email"], "Account Check", $"{user.UserName} want be a agent");

            await _userManager.UpdateAsync(user);
            await _userManager.AddToRoleAsync(user, UserRoles.Agent.ToString());
            await _signInManager.SignOutAsync();

            return true;
        }


        public async Task<UpdateAppUserAgentVM> UpdateAgentAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.Users
                .Include(x => x.AppUserImages).Include(x => x.Agency).FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) throw new NotFoundException("Your request was not found");

            UpdateAppUserAgentVM update = _mapper.Map<UpdateAppUserAgentVM>(user);

            update.Agencys = _mapper.Map<ICollection<IncludeAgencyVM>>(await _agencyRepository.GetAll().ToListAsync());

            return update;
        }

        public async Task<bool> UpdateAgentPostAsync(string id, UpdateAppUserAgentVM update, ModelStateDictionary model, ITempDataDictionary tempData)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.Users
                .Include(x => x.AppUserImages).Include(x => x.Agency).FirstOrDefaultAsync(x => x.Id == id);
            update.Images = _mapper.Map<ICollection<IncludeAppUserImage>>(user.AppUserImages);
            if (user == null) throw new NotFoundException("Your request was not found");
            if (!model.IsValid) return false;
            if (!update.TermsConditions)
            {
                update.Agencys = _mapper.Map<ICollection<IncludeAgencyVM>>(await _agencyRepository.GetAll().ToListAsync());
                model.AddModelError("TermsConditions", "Plese Read and accept rules");
                return false;
            }

            if (!await _agencyRepository.CheckUniqueAsync(x => x.Id == update.AgencyId))
            {
                update.Agencys = _mapper.Map<ICollection<IncludeAgencyVM>>(await _agencyRepository.GetAll().ToListAsync());
                return false;
            }

            if (update.MainPhoto != null)
            {
                if (!update.MainPhoto.ValidateType())
                {
                    update.Agencys = _mapper.Map<ICollection<IncludeAgencyVM>>(await _agencyRepository.GetAll().ToListAsync());
                    model.AddModelError("MainPhoto", "File Not supported");
                    return false;
                }
                if (!update.MainPhoto.ValidataSize())
                {
                    update.Agencys = _mapper.Map<ICollection<IncludeAgencyVM>>(await _agencyRepository.GetAll().ToListAsync());
                    model.AddModelError("MainPhoto", "Image should not be larger than 10 mb");
                    return false;
                }
                AppUserImage mainImage = new AppUserImage
                {
                    //CreatedBy = _http.HttpContext.User.Identity.Name,
                    IsPrimary = true,
                    Url = await _cLoud.FileCreateAsync(update.MainPhoto)
                    //Url = await create.MainPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images")
                };
                user.AppUserImages = new List<AppUserImage> { mainImage };
            }

            if (user.AppUserImages == null) user.AppUserImages = new List<AppUserImage>();

            if (update.ImageIds == null) update.ImageIds = new List<int>();

            ICollection<AppUserImage> remove = user.AppUserImages
                    .Where(pi => pi.IsPrimary == null && !update.ImageIds.Exists(imgId => imgId == pi.Id)).ToList();

            foreach (var image in remove)
            {
                await _cLoud.FileDeleteAsync(image.Url);
                //image.Url.DeleteFile(_env.WebRootPath, "assets", "images");
                user.AppUserImages.Remove(image);
            }

            tempData["Message"] = "";

            if (update.Photos != null)
            {
                foreach (IFormFile photo in update.Photos)
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

                    user.AppUserImages.Add(new AppUserImage
                    {
                        //CreatedBy = _http.HttpContext.User.Identity.Name,
                        IsPrimary = null,
                        Url = await _cLoud.FileCreateAsync(photo)
                        //Url = await photo.CreateFileAsync(_env.WebRootPath, "assets", "images")
                    });
                }
            }
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateAppUserAgentVM, AppUser>()
                    .ForMember(dest => dest.AppUserImages, opt => opt.Ignore());
            });
            var mapper = config.CreateMapper();

            mapper.Map(update, user);

            user.IsActivate = true;

            await _emailService.SendMailAsync(user.Email, "Account Check", "We check your account please wait");
            await _emailService.SendMailAsync(_configuration["AdminSettings:Email"], "Account Check", $"{user.UserName} want be a agent");
            await _emailService.SendMailAsync(_configuration["ModeratorSettings:Email"], "Account Check", $"{user.UserName} want be a agent");

            await _userManager.UpdateAsync(user);
            await _userManager.AddToRoleAsync(user, UserRoles.Agent.ToString());
            await _signInManager.SignOutAsync();

            return true;
        }
    }
}
