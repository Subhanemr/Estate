using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }
    }
}
