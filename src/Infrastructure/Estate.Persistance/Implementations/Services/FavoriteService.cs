using AutoMapper;
using Estate.Application.Abstractions.Repositories;
using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Estate.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Estate.Persistance.Implementations.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IHttpContextAccessor _http;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<AppUser> _userManager;

        public FavoriteService(UserManager<AppUser> userManager, IHttpContextAccessor http, 
            IProductRepository productRepository)
        {
            _userManager = userManager;
            _http = http;
            _productRepository = productRepository;
        }

        public async Task AddWishList(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");
            Product product = await _productRepository.GetByIdAsync(id);
            if (product == null) throw new NotFoundException("Your request was not found");
            ICollection<FavoriteCookieVM> cart;
            ICollection<IncludeProductVM> cartItems;

            if (_http.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.Users
                    .Include(p => p.Favorites).FirstOrDefaultAsync(u => u.Id == _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (appUser == null) throw new NotFoundException("Your request was not found");
                Favorite item = appUser.Favorites.FirstOrDefault(b => b.ProductId == id);
                if (item == null)
                {
                    item = new Favorite
                    {
                        AppUserId = appUser.Id,
                        ProductId = product.Id,
                    };

                    appUser.Favorites.Add(item);
                }
                await _userManager.UpdateAsync(appUser);
            }
            else
            {
                if (_http.HttpContext.Request.Cookies["FavoriteEstate"] is not null)
                {
                    cart = JsonConvert.DeserializeObject<ICollection<FavoriteCookieVM>>(_http.HttpContext.Request.Cookies["FavoriteEstate"]);

                    FavoriteCookieVM item = cart.FirstOrDefault(c => c.Id == id);
                    if (item == null)
                    {
                        FavoriteCookieVM cartCookieItem = new FavoriteCookieVM
                        {
                            Id = id
                        };
                        cart.Add(cartCookieItem);
                    }
                }
                else
                {
                    cart = new List<FavoriteCookieVM>();
                    FavoriteCookieVM cartCookieItem = new FavoriteCookieVM
                    {
                        Id = id
                    };
                    cart.Add(cartCookieItem);
                }

                string json = JsonConvert.SerializeObject(cart);
                _http.HttpContext.Response.Cookies.Append("FavoriteEstate", json, new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(1),
                });

            }
        }

        public async Task DeleteItem(int id)
        {
            if (id <= 0) throw new WrongRequestException("The request sent does not exist");

            if (_http.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.Users
                    .Include(p => p.Favorites).FirstOrDefaultAsync(u => u.Id == _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (appUser == null) throw new NotFoundException("Your request was not found");

                Favorite item = appUser.Favorites.FirstOrDefault(b => b.ProductId == id);

                if (item == null) throw new WrongRequestException("The request sent does not exist");

                appUser.Favorites.Remove(item);

                await _userManager.UpdateAsync(appUser);
            }
            else
            {
                ICollection<FavoriteCookieVM> cart = JsonConvert.DeserializeObject<ICollection<FavoriteCookieVM>>(_http.HttpContext.Request.Cookies["FavoriteEstate"]);

                FavoriteCookieVM item = cart.FirstOrDefault(c => c.Id == id);

                if (item == null) throw new WrongRequestException("The request sent does not exist");

                cart.Remove(item);


                string json = JsonConvert.SerializeObject(cart);
                _http.HttpContext.Response.Cookies.Append("FavoriteEstate", json, new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(1)
                });

            }
        }
    }
}
