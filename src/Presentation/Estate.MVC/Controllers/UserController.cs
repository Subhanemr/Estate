using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Estate.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        public async Task<IActionResult> EditUser()
        {
            return View(await _service.EditUser(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserVM editUser)
        {
            bool result = await _service.EditUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), editUser, ModelState);
            if (!result)
            {
                return View(editUser);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ForgotPassword()
        {
            await _service.ForgotPassword(User.FindFirstValue(ClaimTypes.NameIdentifier), Url);
            return View();
        }
        public IActionResult ChangePassword(string id, string token)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string id, string token, ChangePasswordVM fogotPassword)
        {
            bool result = await _service.ChangePassword(User.FindFirstValue(ClaimTypes.NameIdentifier), token, fogotPassword, ModelState);
            if (!result)
            {
                return View(fogotPassword);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> WishList()
        {
            return View(await _service.GetByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        public async Task<IActionResult> BeAAgent()
        {
            return View(await _service.BeAAgent(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        [HttpPost]
        public async Task<IActionResult> BeAAgent(CreateAppUserAgentVM create)
        {
            bool result = await _service.BeAAgentPost(User.FindFirstValue(ClaimTypes.NameIdentifier), create, ModelState, TempData);
            if (!result)
            {
                return View(create);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        public async Task<IActionResult> UpdateAgent()
        {
            return View(await _service.UpdateAgentAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAgent(UpdateAppUserAgentVM update)
        {
            bool result = await _service.UpdateAgentPostAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), update, ModelState, TempData);
            if (!result)
            {
                return View(update);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        public async Task<IActionResult> Products()
        {
            return View(await _service.GetByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
    }
}
