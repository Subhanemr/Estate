using Estate.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _service;

        public BlogController(IBlogService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, string? returnUrl, int order = 1, int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return View(await _service.GetFilteredAsync(search, 6, page, order));
        }
        public async Task<IActionResult> Detail(int id, string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(await _service.GetByIdAsync(id));
        }
        public async Task<IActionResult> Comment(int blogId, string comment)
        {
            await _service.CommentAsync(blogId, comment, ModelState);

            return RedirectToAction("Detail", "Blog", new { Id = blogId });
        }
        public async Task<IActionResult> Reply(int blogId, int blogCommnetId, string comment)
        {
            await _service.ReplyAsync(blogCommnetId, comment, ModelState);

            return RedirectToAction("Detail", "Blog", new { Id = blogId });
        }
    }
}
