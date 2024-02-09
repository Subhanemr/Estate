using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Index(string? search, int? categoryId, int order = 1, int page = 1)
        {
            return View(model: await _service.GetFilteredAsync(search, 10, page, order, categoryId));
        }
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> DeletedItems(string? search, int? categoryId, int order = 1, int page = 1)
        {
            return View(model: await _service.GetDeleteFilteredAsync(search, 10, page, order, categoryId));
        }
        [Authorize(Roles = "Admin, Moderator")]
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
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Detail(int id)
        {
            GetProductVM get = await _service.GetByIdAsync(id);

            return View(get);
        }

        [Authorize(Roles = "Agent")]
        public IActionResult Create()
        {
            CreateProductVM create = new CreateProductVM();
            _service.CreatePopulateDropdowns(create);
            return View(create);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM create)
        {
            bool result = await _service.CreateAsync(create, ModelState, TempData);
            if (!result)
            {
                return View(create);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Update(int id)
        {
            return View(await _service.UpdateAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateProductVM update)
        {
            bool result = await _service.UpdatePostAsync(id,update, ModelState, TempData);
            if (!result)
            {
                return View(update);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
