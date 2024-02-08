using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ErrorPage(string error)
        {
            if (error == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model: error);
        }
        public IActionResult TermsService()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }
    }
}
