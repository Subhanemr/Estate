using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Estate.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Estate.Persistance.Implementations.Services
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<PaginationVM<AppUser>> GetFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            double count = await _userManager.Users.CountAsync();

            ICollection<AppUser> users = new List<AppUser>();

            switch (order)
            {
                case 1:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.IsActivate == false).OrderBy(x => x.UserName).Skip((page-1)*take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 2:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.IsActivate == false).OrderByDescending(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 3:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.IsActivate == false).OrderBy(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 4:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.IsActivate == false).OrderByDescending(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
            }

            PaginationVM<AppUser> pagination = new PaginationVM<AppUser>
            {
                Take = take,
                Search = search,
                Order = order,
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / take),
                Items = users
            };

            return pagination;
        }

        public async Task<PaginationVM<AppUser>> GetDeleteFilteredAsync(string? search, int take, int page, int order)
        {
            if (page <= 0) throw new WrongRequestException("The request sent does not exist");
            if (order <= 0) throw new WrongRequestException("The request sent does not exist");

            double count = await _userManager.Users.CountAsync();

            ICollection<AppUser> users = new List<AppUser>();

            switch (order)
            {
                case 1:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.IsActivate ==true).OrderBy(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 2:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.IsActivate == true).OrderByDescending(x => x.UserName).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 3:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.IsActivate == true).OrderBy(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
                case 4:
                    users = await _userManager.Users.Where(x => !string.IsNullOrEmpty(search) ? x.UserName.ToLower().Contains(search.ToLower()) : true)
                        .Where(x => x.IsActivate == true).OrderByDescending(x => x.Name).Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync();
                    break;
            }

            PaginationVM<AppUser> pagination = new PaginationVM<AppUser>
            {
                Take = take,
                Search = search,
                Order = order,
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / take),
                Items = users
            };

            return pagination;
        }
    }
}
