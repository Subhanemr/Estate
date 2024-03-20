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
        public async Task<IActionResult> Index(string? search, string? returnUrl, int? categoryId, int? minPrice, int? maxPrice, int? minArea, int? maxArea, int? minBeds, int? minBaths, int order = 1, int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return View(model: await _service.GetFilteredAsync(search, 10, page, order, categoryId, minPrice, maxPrice, minArea, maxArea, minBeds, minBaths));
        }
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> DeletedItems(string? search, string? returnUrl, int? categoryId, int? minPrice, int? maxPrice, int? minArea, int? maxArea, int? minBeds, int? minBaths, int order = 1, int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return View(model: await _service.GetDeleteFilteredAsync(search, 10, page, order, categoryId,minPrice, maxPrice, minArea, maxArea, minBeds, minBaths));
        }
        [Authorize(Roles = "Admin, Moderator")]
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
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Detail(int id, string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(await _service.GetByIdAsync(id));
        }

        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Create(string? returnUrl)
        {
            CreateProductVM create = new CreateProductVM();
            await _service.CreatePopulateDropdowns(create);
            ViewData["ReturnUrl"] = returnUrl;
            return View(create);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM create, string? returnUrl)
        {
            bool result = await _service.CreateAsync(create, ModelState, TempData);
            if (!result)
            {
                return View(create);
            }
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Update(int id, string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(await _service.UpdateAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, string? returnUrl, UpdateProductVM update)
        {
            bool result = await _service.UpdatePostAsync(id,update, ModelState, TempData);
            if (!result)
            {
                return View(update);
            }
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
