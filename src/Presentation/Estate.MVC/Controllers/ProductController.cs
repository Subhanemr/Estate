using Estate.Application.Abstractions.Services;
using Estate.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, string? returnUrl, int? categoryId, int? minPrice, int? maxPrice, int? minArea, int? maxArea, int? minBeds, int? minBaths, int order = 1, int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return View(model: await _service.GetFilteredAsync(search, 10, page, order, categoryId, minPrice, maxPrice, minArea, maxArea, minBeds, minBaths));
        }
        public async Task<IActionResult> Detail(int id, string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(await _service.GetByIdAsync(id));
        }
        public async Task<IActionResult> Comment(int productId, string comment)
        {
            await _service.CommentAsync(productId, comment, TempData);

            return RedirectToAction("Detail", "Product", new { Id = productId });
        }
        public async Task<IActionResult> Reply(int productId, int productCommnetId, string comment)
        {
            await _service.ReplyAsync(productCommnetId, comment, TempData);

            return RedirectToAction("Detail", "Product", new { Id = productId });
        }
        public async Task<IActionResult> AgentMessage(int productId, string message)
        {
            await _service.AgentMessage(productId, message, TempData);

            return RedirectToAction("Detail", "Product", new { Id = productId });
        }
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Delete(int id, string? returnUrl)
        {
            await _service.SoftDeleteAsync(id);
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
