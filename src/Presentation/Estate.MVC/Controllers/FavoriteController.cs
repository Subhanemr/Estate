using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Controllers
{
    public class FavoriteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
