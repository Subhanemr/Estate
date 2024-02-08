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
            await _service.FogotPassword(id, Url);
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(FogotPasswordVM fogotPassword)
        {
            bool result = await _service.ChangePassword(fogotPassword, ModelState);
            if (!result)
            {
                return View(fogotPassword);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> WishList(string id)
        {
            return View();
        }
        public async Task<IActionResult> BeAAgent(string id)
        {
            return View(await _service.BeAAgent(id));
        }
        [HttpPost]
        public async Task<IActionResult> BeAAgent(string id, CreateAppUserAgentVM create)
        {
            bool result = await _service.BeAAgentPost(id,create, ModelState, TempData);
            if (!result)
            {
                return View(create);
            }
            return RedirectToAction("Index", "Home", new { Area = ""});
        }

        public async Task<IActionResult> UpdateAgent(string id)
        {
            return View(await _service.UpdateAgentAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAgent(string id, UpdateAppUserAgentVM update)
        {
            bool result = await _service.UpdateAgentPostAsync(id, update, ModelState, TempData);
            if (!result)
            {
                return View(update);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        public async Task<IActionResult> Products(string id)
        {
            return View(await _service.GetByIdAsync(id));
        }
    }
}
