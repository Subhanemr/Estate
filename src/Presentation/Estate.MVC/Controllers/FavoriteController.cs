using Estate.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _service;

        public FavoriteController(IFavoriteService service)
        {
            _service = service;
        }

        public async Task<IActionResult> WishList()
        {
            return View(await _service.WishList());
        }

        public async Task<IActionResult> AddWishList(int id)
        {
            await _service.AddWishList(id);
            return RedirectToAction("Index", "Home", new { Area = ""});
        }

        public async Task<IActionResult> DeleteItem(int id)
        {
            await _service.DeleteItem(id);
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
