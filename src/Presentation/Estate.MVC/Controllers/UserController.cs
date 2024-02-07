using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
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

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetByUserNameAsync(User.Identity.Name));
        }
        public async Task<IActionResult> EditUser(string id)
        {
            return View(await _service.EditUser(id));
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(string id, EditUserVM editUser)
        {
            bool result = await _service.EditUserAsync(id, editUser, ModelState);
            if(!result)
            {
                return View(editUser);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> FogotPassword(string id)
        {
            return View();
        }
        public async Task<IActionResult> ChangePassword(string id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string id, FogotPasswordVM fogotPassword)
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
