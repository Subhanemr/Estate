using Estate.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string id)
        {
            return View(await _service.GetByIdAsync(id));
        }
        public async Task<IActionResult> EditUser(string id)
        {
            return View();
        }
        public async Task<IActionResult> FogotPassword(string id)
        {
            return View();
        }
        public async Task<IActionResult> ChangePassword(string id)
        {
            return View();
        }
        public async Task<IActionResult> WishList(string id)
        {
            return View();
        }
        public async Task<IActionResult> BeAAgent(string id)
        {
            return View();
        }
    }
}
