    using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Moderator")]
    [AutoValidateAntiforgeryToken]
    public class AgencyController : Controller
    {
        private readonly IAgencyService _service;

        public AgencyController(IAgencyService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, string? returnUrl, int order = 1, int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return View(model: await _service.GetFilteredAsync(search, 10, page, order));
        }

        public async Task<IActionResult> DeletedItems(string? search, string? returnUrl, int order = 1, int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return View(model: await _service.GetDeleteFilteredAsync(search, 10, page, order));
        }

        public IActionResult Create(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAgencyVM create, string? returnUrl)
        {
            bool result = await _service.CreateAsync(create, ModelState);
            if (!result)
            {
                return View(create);
            }
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id, string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(await _service.UpdateAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, string? returnUrl, UpdateAgencyVM update)
        {
            bool result = await _service.UpdatePostAsync(id, update, ModelState);
            if (!result)
            {
                return View(update);
            }
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> SoftDelete(int id, string? returnUrl)
        {
            await _service.SoftDeleteAsync(id);
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReverseSoftDelete(int id, string? returnUrl)
        {
            await _service.ReverseSoftDeleteAsync(id);
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(DeletedItems));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, string? returnUrl)
        {
            await _service.DeleteAsync(id);
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(DeletedItems));
        }


        public async Task<IActionResult> Detail(int id, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(await _service.GetByIdAsync(id));
        }
    }
}
