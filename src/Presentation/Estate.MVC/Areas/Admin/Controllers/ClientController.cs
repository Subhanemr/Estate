using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Moderator")]
    [AutoValidateAntiforgeryToken]
    public class ClientController : Controller
    {
        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, int order = 1, int page = 1)
        {
            return View(model: await _service.GetFilteredAsync(search, 10, page, order));
        }

        public async Task<IActionResult> DeletedItems(string? search, int order = 1, int page = 1)
        {
            return View(model: await _service.GetDeleteFilteredAsync(search, 10, page, order));
        }
        public async Task<IActionResult> SoftDelete(int id)
        {
            await _service.SoftDeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReverseSoftDelete(int id)
        {
            await _service.ReverseSoftDeleteAsync(id);

            return RedirectToAction(nameof(DeletedItems));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(DeletedItems));
        }

        public async Task<IActionResult> Detail(int id)
        {
            GetClientVM get = await _service.GetByIdAsync(id);

            return View(get);
        }
    }
}
