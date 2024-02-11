using Estate.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, int? categoryId, int? minPrice, int? maxPrice, int? minArea, int? maxArea, int? minBeds, int? minBaths, int order = 1, int page = 1)
        {
            return View(model: await _service.GetFilteredAsync(search, 10, page, order, categoryId, minPrice, maxPrice, minArea, maxArea, minBeds, minBaths));
        }
        public async Task<IActionResult> Detail(int id)
        {
            return View(await _service.GetByIdAsync(id));
        }
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.SoftDeleteAsync(id);

            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
