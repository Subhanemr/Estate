using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
