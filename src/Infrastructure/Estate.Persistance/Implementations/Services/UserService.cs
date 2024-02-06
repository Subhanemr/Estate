using AutoMapper;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Estate.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Estate.Persistance.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
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
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true).Where(x => x.UserName != "admin")
                        .Where(x => x.IsActivate == false).OrderBy(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 2:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true).Where(x => x.UserName != "admin")
                        .Where(x => x.IsActivate == false).OrderByDescending(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 3:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true).Where(x => x.UserName != "admin")
                        .Where(x => x.IsActivate == false).OrderBy(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 4:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true).Where(x => x.UserName != "admin")
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
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true).Where(x => x.UserName != "admin")
                        .Where(x => x.IsActivate == true).OrderBy(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 2:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true).Where(x => x.UserName != "admin")
                        .Where(x => x.IsActivate == true).OrderByDescending(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 3:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true).Where(x => x.UserName != "admin")
                        .Where(x => x.IsActivate == true).OrderBy(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 4:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true).Where(x => x.UserName != "admin")
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

        public async Task<GetAppUserVM> GetByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.Users
                .Include(x => x.AppUserImages).Include(x => x.AgencyAppUsers)
                .Include(x => x.Products).ThenInclude(x => x.Category)
                .Include(x => x.Products).ThenInclude(x => x.ProductImages).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) throw new NotFoundException("Your request was not found");

            GetAppUserVM get = _mapper.Map<GetAppUserVM>(user);

            return get;
        }

        public async Task<GetAppUserVM> GetByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new WrongRequestException("The request sent does not exist");
            AppUser user = await _userManager.Users
                .Include(x => x.AppUserImages).Include(x => x.AgencyAppUsers)
                .Include(x => x.Products).ThenInclude(x => x.Category)
                .Include(x => x.Products).ThenInclude(x => x.ProductImages).AsNoTracking().FirstOrDefaultAsync(x => x.UserName == userName);
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
    }
}
