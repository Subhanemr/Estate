using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Estate.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Estate.MVC.ViewComponents
{
    public class FavoriteViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _http;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public FavoriteViewComponent(IHttpContextAccessor http, UserManager<AppUser> userManager, IMapper mapper)
        {
            _http = http;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ICollection<FavoriteItemVM> wishLists = new List<FavoriteItemVM>();

            if (_http.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.Users.Include(x => x.Favorites).FirstOrDefaultAsync(x => x.Id == _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (user == null) throw new NotFoundException("Your request was not found");

                foreach (Favorite item in user.Favorites)
                {
                    wishLists.Add(new FavoriteItemVM
                    {
                        Id = item.ProductId
                    });
                }
            }
            else
            {
                if (_http.HttpContext.Request.Cookies["FavoriteEstate"] is not null)
                {
                    ICollection<FavoriteCookieVM> wishes = JsonConvert.DeserializeObject<ICollection<FavoriteCookieVM>>(_http.HttpContext.Request.Cookies["FavoriteEstate"]);
                    foreach (FavoriteCookieVM wishListCookieItem in wishes)
                    {
                        FavoriteItemVM wish = new FavoriteItemVM
                        {
                            Id = wishListCookieItem.Id
                        };
                        wishLists.Add(wish);
                    }
                }
            }
            return View(wishLists);
        }
    }
}
